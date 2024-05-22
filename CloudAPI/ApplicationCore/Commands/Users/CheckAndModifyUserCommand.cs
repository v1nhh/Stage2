using AutoMapper;
using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using CTAMSharedLibrary.Utilities;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemModule.ApplicationCore.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Queries.Users;


namespace CloudAPI.ApplicationCore.Commands.Users
{
    public class CheckAndModifyUserCommand : IRequest<UserWebDTO>
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string LoginCode { get; set; }

        public string CardCode { get; set; }

        public string PhoneNumber { get; set; }

        public string PinCode { get; set; }

        public string LanguageCode { get; set; }

        public List<int> AddRolesIDs { get; set; }

        public List<int> RemoveRolesIDs { get; set; }

        public List<int> AddPersonalItemIDs { get; set; }

        public List<int> RemovePersonalItemIDs { get; set; }

        public int MailMarkupTemplateID { get; set; }
    }

    public class CheckAndModifyUserHandler : IRequestHandler<CheckAndModifyUserCommand, UserWebDTO>
    {
        private readonly ILogger<CheckAndModifyUserHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CheckAndModifyUserHandler(ILogger<CheckAndModifyUserHandler> logger, MainDbContext context, IMediator mediator, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<UserWebDTO> Handle(CheckAndModifyUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndModifyUserHandler called");

            if (request.Name != null && string.IsNullOrWhiteSpace(request.Name)) // New username has been given, but only whitespace
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_emptyName);
            }

            var checkQuery = new CheckRoleItemTypeDependencyQuery(request.UID, request.RemoveRolesIDs);
            var cannotRemoveRoleDescriptions = await _mediator.Send(checkQuery);
            if (cannotRemoveRoleDescriptions.Count > 0)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_roleDependencyInPossession,
                                          new Dictionary<string, string> { { "roles", string.Join('\n', cannotRemoveRoleDescriptions) } });
            }

            // For different isolation levels see: https://docs.microsoft.com/en-us/dotnet/api/system.transactions.isolationlevel?redirectedfrom=MSDN&view=netcore-3.1
            // Example usage of isolation levels:
            //     new TransactionScope(TransactionScopeOption.Required,
            //            new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
            //            TransactionScopeAsyncFlowOption.Enabled))
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var changes = await ModifyUser(request);
                var user = await _mediator.Send(new GetUserByUidQuery(request.UID));

                var emailLines = convertChangesToEmailString(changes);

                await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.UserModified.GetName(),
                    LanguageCode = user.LanguageCode,
                    MailMarkupTemplateID = request.MailMarkupTemplateID,
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name },
                        { "changes", emailLines },
                    },
                });
                scope.Complete();
                return _mapper.Map<UserWebDTO>(user);
            }
        }
        public async Task<List<(string key, string value)>> ModifyUser(CheckAndModifyUserCommand request)
        {
            var user = await _context.CTAMUser().Where(user => user.UID.Equals(request.UID)).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "uid", request.UID } });
            }

            if (request.Name != null && string.IsNullOrWhiteSpace(request.Name))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_emptyName);
            }

            if (!string.IsNullOrEmpty(request.LoginCode) && request.LoginCode.Length != 6)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_invalidLoginCode,
                                          new Dictionary<string, string> { { "loginCode", request.LoginCode }, { "loginCodeSize", "6" } });
            }

            if (!string.IsNullOrEmpty(request.LoginCode) && await _context.CTAMUser().AnyAsync(u => u.LoginCode == request.LoginCode))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_duplicateLoginCode,
                                          new Dictionary<string, string> { { "loginCode", request.LoginCode } });
            }

            if (!string.IsNullOrEmpty(request.CardCode) && await _context.CTAMUser().AnyAsync(u => u.CardCode == request.CardCode && u.UID != user.UID))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_duplicateCardCode,
                                          new Dictionary<string, string> { { "cardCode", request.CardCode } });
            }

            if (request.Email != null && string.IsNullOrWhiteSpace(request.Email))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_emptyEmail);
            }

            if (!string.IsNullOrEmpty(request.Email) && await _context.CTAMUser().AnyAsync(u => u.Email == request.Email && u.UID != user.UID))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_duplicateEmail,
                                          new Dictionary<string, string> { { "email", request.Email } });
            }

            var changes = ModifySingleFields(user, request);

            if (changes.Count > 0)
            {
                changes.Add(("name", user.Name));
                changes.Add(("email", user.Email));
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedUser), changes.ToArray());
            }

            await ModifyRoles(user, request, changes);
            await ModifyPersonalItems(user, request, changes);
            await _context.SaveChangesAsync();

            return changes;
        }

        public List<(string key, string value)> ModifySingleFields(CTAMUser user, CheckAndModifyUserCommand request)
        {
            var changes = new List<(string key, string value)>();

            if (request.Name != null && !request.Name.Equals(user.Name))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing Name from '{user.Name}' to '{request.Name}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldName", user.Name),
                    ("newName", request.Name)
                });
                user.Name = request.Name;
            }
            if (request.Email != null && !request.Email.Equals(user.Email))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing Email from '{user.Email}' to '{request.Email}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldEmail", user.Email),
                    ("newEmail", request.Email)
                });
                user.Email = request.Email;
            }
            if (request.LoginCode != null && !request.LoginCode.Equals(user.LoginCode))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing LoginCode from '{user.LoginCode}' to '{request.LoginCode}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldLoginCode", user.LoginCode),
                    ("newLoginCode", request.LoginCode)
                });
                user.LoginCode = request.LoginCode;
            }
            if (request.CardCode != null && !request.CardCode.Equals(user.CardCode))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing CardCode from '{user.CardCode}' to '{request.CardCode}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldCardCode", user.CardCode),
                    ("newCardCode", request.CardCode)
                });
                user.CardCode = request.CardCode;
            }
            if (request.PhoneNumber != null && !request.PhoneNumber.Equals(user.PhoneNumber))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing PhoneNumber from '{user.PhoneNumber}' to '{request.PhoneNumber}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldPhoneNumber", user.PhoneNumber),
                    ("newPhoneNumber", request.PhoneNumber)
                });
                user.PhoneNumber = request.PhoneNumber;
            }
            if (request.PinCode != null && !request.PinCode.Equals(user.PinCode))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing PinCode from '{user.PinCode}' to '{request.PinCode}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldPinCode", user.PinCode),
                    ("newPinCode", request.PinCode)
                });
                user.PinCode = request.PinCode;
            }
            if (request.LanguageCode != null && !request.LanguageCode.Equals(user.LanguageCode))
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Changing LanguageCode from '{user.LanguageCode}' to '{request.LanguageCode}'");
                changes.AddRange(new List<(string key, string value)>()
                {
                    ("oldLanguageCode", user.LanguageCode),
                    ("newLanguageCode", request.LanguageCode)
                });
                user.LanguageCode = request.LanguageCode;
            }
            return changes;
        }

        public async Task ModifyRoles(CTAMUser user, CheckAndModifyUserCommand request, List<(string key, string value)> changes)
        {
            if (request.RemoveRolesIDs != null && request.RemoveRolesIDs.Count > 0)
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Removing roles: '{string.Join(',', request.RemoveRolesIDs)}");
                var rolesToRemove = await _context.CTAMUser_Role()
                    .Where(ur => user.UID.Equals(ur.CTAMUserUID) && request.RemoveRolesIDs.Contains(ur.CTAMRoleID))
                .Include(ur => ur.CTAMRole)
                .ThenInclude(r => r.CTAMRole_Permission)
                .ThenInclude(rp => rp.CTAMPermission)
                    .ToListAsync();

                var readPermissionID = await _context.CTAMRole_Permission().Include(rp => rp.CTAMPermission).Where(rp => rp.CTAMPermission.Description.Equals("Read")).Select(rp => rp.CTAMPermissionID).FirstOrDefaultAsync();
                var writePermissionID = await _context.CTAMRole_Permission().Include(rp => rp.CTAMPermission).Where(rp => rp.CTAMPermission.Description.Equals("Write")).Select(rp => rp.CTAMPermissionID).FirstOrDefaultAsync();
                var addedPermissions = new List<int>();
                if (request.AddRolesIDs != null && request.AddRolesIDs.Count > 0)
                {
                    // Collect all permissions from roles to be added to user, that are read or write permissions.
                    addedPermissions = await _context.CTAMRole().AsNoTracking()
                    .Where(role => request.AddRolesIDs.Contains(role.ID))
                    .SelectMany(role => role.CTAMRole_Permission)
                    .Select(rp => rp.CTAMPermissionID)
                    .Where(p => p == readPermissionID || p == writePermissionID)
                    .ToListAsync();
                }
                // Get all user permission combinations from roles, which are only of read & write permissions.
                var userPermissionTuples = await _context.CTAMUser_Role().Where(ur => !(ur.CTAMUserUID.Equals(user.UID) && request.RemoveRolesIDs.Contains(ur.CTAMRoleID)))
                    .Join(_context.CTAMRole_Permission()
                        .Where(rp => rp.CTAMPermissionID == readPermissionID || rp.CTAMPermissionID == writePermissionID), ur => ur.CTAMRoleID, rp => rp.CTAMRoleID, (ur, rp) => new { User = ur.CTAMUserUID, Permission = rp.CTAMPermissionID })
                    .ToListAsync();
                userPermissionTuples.AddRange(addedPermissions.Select(ap => new { User = user.UID, Permission = ap }));

                // Get distinct users to check for latest user with access
                var users = userPermissionTuples.Select(upt => upt.User).Distinct().ToList();
                // Search for users with both read and write access
                var anotherUserWithAccess = users.Where(u => userPermissionTuples.Where(upt => upt.User.Equals(u) && upt.Permission == readPermissionID).Any()
                                                            && userPermissionTuples.Where(upt => upt.User.Equals(u) && upt.Permission == writePermissionID).Any());

                if (!anotherUserWithAccess.Any())
                {
                    throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.users_apiExceptions_lastUserWithReadAndWriteAccessRole);
                }

                changes.Add(("removedRoleDescriptions", string.Join(',', rolesToRemove.Select(ur => ur.CTAMRole.Description))));

                _context.CTAMUser_Role().RemoveRange(rolesToRemove);

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedRolesFromUser),
                    ("roles", string.Join(", ", rolesToRemove.Select(ur => ur.CTAMRole.Description))), 
                    ("name", user.Name),
                    ("email", user.Email));
            }

            if (request.AddRolesIDs != null && request.AddRolesIDs.Count > 0)
            {
                _logger.LogInformation($"UserUID='{request.UID}' > Adding roles: '{string.Join(", ", request.AddRolesIDs)}");
                var existingRoleIDsToAdd = await _context.CTAMRole().AsNoTracking().Where(r => request.AddRolesIDs.Contains(r.ID)).Select(r => r.ID).ToListAsync();
                var rolesToAdd = existingRoleIDsToAdd.Select(roleId => new CTAMUser_Role()
                {
                    CTAMRoleID = roleId,
                    CTAMUserUID = user.UID,
                }).ToArray();

                var alreadyExistRole = await _context.CTAMUser_Role()
                                                     .Include(ur => ur.CTAMRole)
                                                     .Where(ur => ur.CTAMUserUID == user.UID && existingRoleIDsToAdd.Contains(ur.CTAMRoleID))
                                                     .Select(ur => ur.CTAMRole)
                                                     .FirstOrDefaultAsync();
                if (alreadyExistRole != null)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.roles_apiExceptions_userRoleDuplicate,
                                              new Dictionary<string, string> { { "uid", user.UID },
                                              { "roleID", alreadyExistRole.ID.ToString() },
                                              { "roleDescription", alreadyExistRole.Description } });
                }

                await _context.CTAMUser_Role().AddRangeAsync(rolesToAdd);

                var addedRoles = await _context.CTAMRole().AsNoTracking()
                    .Where(role => request.AddRolesIDs.Contains(role.ID))
                    .Select(role => role.Description)
                    .ToListAsync();

                changes.Add(("addedRoleDescriptions", string.Join(", ", addedRoles)));

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedRolesToUser),
                    ("roles", string.Join(", ", addedRoles)), 
                    ("name", user.Name),
                    ("email", user.Email));
            }
        }

        public async Task ModifyPersonalItems(CTAMUser user, CheckAndModifyUserCommand request, List<(string key, string value)> changes)
        {
            if (request.RemovePersonalItemIDs != null && request.RemovePersonalItemIDs.Count > 0)
            {
                var excludedItems = await _context.CTAMUserPersonalItem()
                    .Include(i => i.Item)
                    .Where(upi => request.RemovePersonalItemIDs.Contains(upi.ItemID) && !(upi.Item.Status == ItemStatus.INITIAL || upi.Item.Status == ItemStatus.NOT_ACTIVE))
                    .ToListAsync();

                foreach (var excludedItem in excludedItems)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_removePersonalItemStatusIncorrect,
                                              new Dictionary<string, string> { { "userUID", request.UID },
                                                                               { "description", excludedItem.Item.Description },
                                                                               { "status", excludedItem.Item.Status.ToString() } });
                }

                var personalItemsToRemove = await _context.CTAMUserPersonalItem()
                    .Include(i => i.Item)
                    .Where(upi => user.UID.Equals(upi.CTAMUserUID) && request.RemovePersonalItemIDs.Contains(upi.ItemID) && (upi.Item.Status == ItemStatus.INITIAL || upi.Item.Status == ItemStatus.NOT_ACTIVE))
                    .ToListAsync();
                _logger.LogInformation($"UserUID='{request.UID}' > Removing personalItems: '{string.Join(',', personalItemsToRemove.Select(upi => upi.ItemID))}");

                changes.Add(("removedPersonalItemDescriptions", string.Join(", ", personalItemsToRemove.Select(upi => upi.Item.Description))));
                
                _context.CTAMUserPersonalItem().RemoveRange(personalItemsToRemove);

                var itemsInPossesion = await _context.CTAMUserInPossession()
                    .Where(uip => user.UID.Equals(uip.CTAMUserUIDOut) && request.RemovePersonalItemIDs.Contains(uip.ItemID))
                    .ToListAsync();

                foreach (var itemInPossesion in itemsInPossesion)
                {
                    if (itemsInPossesion != null)
                    {
                        itemInPossesion.Status = UserInPossessionStatus.Returned;
                        itemInPossesion.CTAMUserUIDIn = user.UID;
                        itemInPossesion.CTAMUserNameIn = user.Name;
                        itemInPossesion.CTAMUserEmailIn = user.Email;
                        itemInPossesion.InDT = DateTime.UtcNow;
                        itemInPossesion.CabinetNameIn = "Unassigned Personal Item";
                    }
                }

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedPersonalItemsFromUser),
                    ("deletedPersonalItems", string.Join(", ", personalItemsToRemove.Select(upi => upi.Item.Description))), 
                    ("name", user.Name),
                    ("email", user.Email));
            }

            if (request.AddPersonalItemIDs != null && request.AddPersonalItemIDs.Count > 0)
            {
                var items = await _context.Item()
                    .Where(i => request.AddPersonalItemIDs.Contains(i.ID))
                    .ToListAsync();

                if (items.Count < request.AddPersonalItemIDs.Count)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_addPersonalItemNotFound);
                }

                if (items.Any(i => (i.Status != ItemStatus.INITIAL) && (i.Status != ItemStatus.NOT_ACTIVE)))
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_addPersonalItemStatusIncorrect);
                }

                if (_context.CTAMUserPersonalItem().Any(upi => request.AddPersonalItemIDs.Contains(upi.ItemID) ||
                                                              (upi.ReplacementItemID != null &&
                                                              request.AddPersonalItemIDs.Contains((int)upi.ReplacementItemID))))
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_personalItemAlreadyInUse);
                }

                _logger.LogInformation($"UserUID='{request.UID}' > Adding personal items: '{string.Join(", ", request.AddPersonalItemIDs)}");

                foreach (var item in items)
                {
                    item.Status = ItemStatus.ACTIVE;
                }

                var personalItemsToAdd = request.AddPersonalItemIDs.Select(personalItemId => new CTAMUserPersonalItem()
                {
                    ItemID = personalItemId,
                    CTAMUserUID = user.UID
                })
                    .ToArray();

                changes.Add(("addedPersonalItemDescriptions", string.Join(", ", items.Select(upi => upi.Description))));

                await _context.CTAMUserPersonalItem().AddRangeAsync(personalItemsToAdd);

                var addPersonalItemInPossession = request.AddPersonalItemIDs.Select(personalItemId => new CTAMUserInPossession()
                {
                    ItemID = personalItemId,
                    Status = UserInPossessionStatus.Picked,
                    CTAMUserUIDOut = user.UID,
                    CTAMUserNameOut = user.Name,
                    CTAMUserEmailOut = user.Email,
                    OutDT = DateTime.UtcNow,
                    CreatedDT = DateTime.UtcNow,
                    CabinetNameOut = "Assigned Personal Item"
                })
                    .ToArray();
                await _context.CTAMUserInPossession().AddRangeAsync(addPersonalItemInPossession);

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedPersonalItemsToUser),
                    ("addedPersonalItems", string.Join(", ", items.Select(upi => upi.Description))), 
                    ("name", user.Name),
                    ("email", user.Email));
            }
        }

        private string convertChangesToEmailString(List<(string key, string value)> changes)
        {
            var emailChangedValueLines = new List<string>();
            var currentThreadCulture = Thread.CurrentThread.CurrentUICulture;

            if (changes.Any(item => item.key.Equals("newLanguageCode")))
            {
                // map e.g. 'nl-NL' to 'Nederlands' first.
                var indexOldLanguageCode = changes.FindIndex(item => item.key.Equals("oldLanguageCode"));
                var indexNewLanguageCode = changes.FindIndex(item => item.key.Equals("newLanguageCode"));

                // Set the new culture
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(changes[indexNewLanguageCode].value);

                // Create the CultureInfo objects after setting the new culture
                var oldCultureInfo = new CultureInfo(changes[indexOldLanguageCode].value);
                var newCultureInfo = new CultureInfo(changes[indexNewLanguageCode].value);

                // Now retrieve the DisplayNames
                var oldLanguageDisplayName = oldCultureInfo.DisplayName;
                var newLanguageDisplayName = newCultureInfo.DisplayName;

                // Update the changes
                changes[indexOldLanguageCode] = (changes[indexOldLanguageCode].key, oldLanguageDisplayName);
                changes[indexNewLanguageCode] = (changes[indexNewLanguageCode].key, newLanguageDisplayName);

                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_languageCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newEmail")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_email, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newName")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_name, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newCardCode")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_cardCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newLoginCode")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_loginCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newPhoneNumber")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_phoneNumber, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newPinCode")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_pinCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("addedRoleDescriptions")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_addedRoles, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("removedRoleDescriptions")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_removedRoles, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("addedPersonalItemDescriptions")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_addedPersonalItems, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("removedPersonalItemDescriptions")))
            {
                emailChangedValueLines.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_removedPersonalItems, changes.ToArray()));
            }
            var result = string.Join("", emailChangedValueLines.Select(line => $"<tr><td><b>{line}</b></td></tr>"));

            Thread.CurrentThread.CurrentUICulture = currentThreadCulture;
            return result;
        }
    }
}
