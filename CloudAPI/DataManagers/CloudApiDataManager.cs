using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudApiModule.ApplicationCore.DataManagers
{
    public class CloudApiDataManager
    {
        private readonly ILogger<CloudApiDataManager> _logger;
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;

        public CloudApiDataManager(MainDbContext context, ILogger<CloudApiDataManager> logger, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _managementLogger = managementLogger;
        }

        #region Roles

        /// <summary>
        /// Check which CabinetStock entries should be deleted. If removeItemTypeIDs and removeCabinetNumbers are both null
        /// all CabinetStock entries that are only "referenced" from this role are returned. If one or both lists are given the combinations
        /// with all of the other elements are made. So if one cabinet X will be removed and current ItemType list is IT1, IT2, ... the
        /// CabinetStock combinations X-IT1, X-IT2, ... should be checked and for ItemTypes vice versa.
        /// If a to be deleted CabinetStock has ActualStock an exception is thrown.
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="removeItemTypeIDs"></param>
        /// <param name="removeCabinetNumbers"></param>
        /// <returns></returns>
        public async Task CheckAndDeleteCabinetStock(int roleID, IEnumerable<int> removeItemTypeIDs, IEnumerable<string> removeCabinetNumbers, bool isDeleteRole)
        {
            var allCabinetNumbers = await _context.CTAMRole_Cabinet().Where(rc => rc.CTAMRoleID == roleID).Select(rc => rc.CabinetNumber).ToListAsync();
            var allItemTypeIDs = await _context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == roleID).Select(rit => rit.ItemTypeID).ToListAsync();

            IQueryable<CabinetStock> cabinetStocks;

            if (removeItemTypeIDs != null || removeCabinetNumbers != null )
            {
                // Limit CabinetStock to Cabinets or ItemTypes that might be deleted
                cabinetStocks = (from cs in _context.CabinetStock()
                                 join rc in _context.CTAMRole_Cabinet().Where(rc => rc.CTAMRoleID == roleID && (removeCabinetNumbers == null || removeCabinetNumbers.Contains(rc.CabinetNumber)))
                                 on cs.CabinetNumber equals rc.CabinetNumber
                                 join rit in _context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == roleID && (removeItemTypeIDs == null || removeItemTypeIDs.Contains(rit.ItemTypeID)))
                                 on cs.ItemTypeID equals rit.ItemTypeID
                                 select cs);
            }
            else if (isDeleteRole)
            {   // Inspect all crosscombinations of Cabinets and ItemTypes for this role
                cabinetStocks = (from cs in _context.CabinetStock()
                                 join rc in _context.CTAMRole_Cabinet().Where(rc => rc.CTAMRoleID == roleID)
                                 on cs.CabinetNumber equals rc.CabinetNumber
                                 join rit in _context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == roleID)
                                 on cs.ItemTypeID equals rit.ItemTypeID
                                 select cs);
            }
            else
            {
                return; // Modify role, but no removal of ItemTypes or CabinetNumbers, so nothing changes for CabinetStock
            }

            var otherStocks = (from cs in cabinetStocks
                                   //where ur.CTAMUserUID.Equals(request.CTAMUserUID)
                               join rc in _context.CTAMRole_Cabinet() //.Where(rc => rc.CTAMRoleID != roleID)
                               on cs.CabinetNumber equals rc.CabinetNumber
                               join rit in _context.CTAMRole_ItemType()
                               on cs.ItemTypeID equals rit.ItemTypeID
                               where rc.CTAMRoleID == rit.CTAMRoleID
                               group new {cs} by new {cs.CabinetNumber, cs.ItemTypeID} into gr
                               select new { gr.Key.CabinetNumber, gr.Key.ItemTypeID, Count = gr.Count() });

            var stocksToBeDeleted = (from os in otherStocks.Where(os => os.Count == 1)
                                     join cs in cabinetStocks
                                     on new { os.CabinetNumber, os.ItemTypeID } equals new { cs.CabinetNumber, cs.ItemTypeID }
                                     select cs);

            var stockThatCannotBeDeleted = await stocksToBeDeleted.Where(cs => cs.ActualStock > 0).FirstOrDefaultAsync();
            if (stockThatCannotBeDeleted != null)
            {
                var itemTypeDesc = await _context.ItemType().Where(it => it.ID == stockThatCannotBeDeleted.ItemTypeID).Select(it => it.Description).SingleOrDefaultAsync();
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.roles_apiExceptions_deleteCabinetStockForCabinetWithItemTypes,
                                          new Dictionary<string, string> { { "cabinetNumber", stockThatCannotBeDeleted.CabinetNumber },
                                                                           { "itemTypeDescription", itemTypeDesc } });
            }

            if (stocksToBeDeleted.Count() > 0)
            {
                _context.CabinetStock().RemoveRange(stocksToBeDeleted);
            }
        }

        public async Task DeleteRole(int roleID)
        {
            _logger.LogInformation("DeleteRole called");
            var role = await _context.CTAMRole().FindAsync(roleID);
            if (role == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", roleID.ToString() } });
            }

            var readPermissionID = await _context.CTAMPermission().Where(p => p.Description.Equals("Read")).Select(p => p.ID).FirstOrDefaultAsync();
            var writePermissionID = await _context.CTAMPermission().Where(p => p.Description.Equals("Write")).Select(p => p.ID).FirstOrDefaultAsync();

            // Get all user x permission tuples where permission is read or write
            var userPermissionTuples = await _context.CTAMUser_Role().Where(ur => ur.CTAMRoleID != roleID)
                    .Join(_context.CTAMRole_Permission()
                        .Where(rp => rp.CTAMPermissionID == readPermissionID || rp.CTAMPermissionID == writePermissionID), ur => ur.CTAMRoleID, rp => rp.CTAMRoleID, (ur, rp) => new { User = ur.CTAMUserUID, Permission = rp.CTAMPermissionID })
                    .ToListAsync();

            // Get distinct users to check for latest user with access
            var users = userPermissionTuples.Select(upt => upt.User).Distinct().ToList();

            // Search for users with both read and write access
            var usersWithAccess = users.Where(u => userPermissionTuples.Where(upt => upt.User.Equals(u) && upt.Permission == readPermissionID).Any()
                                                        && userPermissionTuples.Where(upt => upt.User.Equals(u) && upt.Permission == writePermissionID).Any());

            if (!usersWithAccess.Any())
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_lastRoleWithReadAndWriteAccess);
            }

            _context.CTAMRole().Remove(role);
            await _context.SaveChangesAsync();

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedRole), 
                ("description", role.Description));
        }

        public async Task<CTAMRole> ModifyRole(int roleID, string description, string validFromDT, string validUntilDT,
                                              IEnumerable<int> addPermissionIDs, IEnumerable<int> removePermissionIDs,
                                              IEnumerable<string> addUserUIDs, IEnumerable<string> removeUserUIDs)
        {
            _logger.LogInformation("ModifyRole called");
            var role = await _context.CTAMRole().Where(role => role.ID == roleID).FirstOrDefaultAsync();
            if (role == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", roleID.ToString() } });
            }

            var changes = await ModifySingleRoleFields(role, description, validFromDT, validUntilDT);

            if (changes.Count > 0)
            {
                changes.Add(("description", role.Description));
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedRole), changes.ToArray());
            }

            await ModifyRolePermissions(role, addPermissionIDs, removePermissionIDs);
            await ModifyRoleUsers(role, addUserUIDs, removeUserUIDs);
            await _context.SaveChangesAsync();

            role = await _context.CTAMRole().AsNoTracking().Where(role => role.ID == roleID)
                .Include(role => role.CTAMRole_Permission)
                    .ThenInclude(rp => rp.CTAMPermission)
                .FirstOrDefaultAsync();

            return role;
        }

        private async Task<List<(string key, string value)>> ModifySingleRoleFields(CTAMRole role, string description, string validFromDT, string validUntilDT)
        {
            var changes = new List<(string key, string value)>();

            if (description != null)
            {
                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.roles_apiExceptions_emptyDescription);
                }

                var roleDescriptionCheck = await _context.CTAMRole().AsNoTracking().Where(r => r.ID != role.ID && r.Description == description).FirstOrDefaultAsync();
                if (roleDescriptionCheck == null)
                {
                    if (role.Description != null && !role.Description.Equals(description))
                    {
                        _logger.LogInformation($"RoleID={role.ID} > Modified description from '{role.Description}' to '{description}'");
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldDescription", role.Description),
                            ("newDescription", description)
                        });
                    }
                    else if (role.Description == null) // suggests that a new role is created.
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("newDescription", description)
                        });
                    }
                    role.Description = description;

                }
                else
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_duplicateDescription,
                                              new Dictionary<string, string> { { "description", description } });
                }
            }
            if (validFromDT != null)
            {
                if (role.ValidFromDT != null) {
                    if (DateTime.TryParse(validFromDT, out DateTime dt))
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidFromDT", role.ValidFromDT.ToString()),
                            ("newValidFromDT", DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToString())
                        });
                        role.ValidFromDT = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    }
                    else
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidFromDT", role.ValidFromDT.ToString()),
                            ("newValidFromDT", string.Empty) // empty string is a "none". Will be translated in the profile mapper.
                        }); ;
                        role.ValidFromDT = null;
                    }
                } 
                else
                {
                    if (DateTime.TryParse(validFromDT, out DateTime dt))
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidFromDT", string.Empty), // empty string is a "none". Will be translated in the profile mapper.
                            ("newValidFromDT", DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToString())
                        });
                        role.ValidFromDT = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    }
                    else
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidFromDT", string.Empty),
                            ("newValidFromDT", "error") // TODO: should be handled as a user input error?
                        });
                        role.ValidFromDT = null;
                    }
                }
                _logger.LogInformation($"RoleID={role.ID} > Modified valid FROM date");
            }
            if (validUntilDT != null)
            {
                if (role.ValidUntilDT != null)
                {
                    if (DateTime.TryParse(validUntilDT, out DateTime dt))
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidUntilDT", role.ValidUntilDT.ToString()),
                            ("newValidUntilDT", DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToString())
                        });
                        role.ValidUntilDT = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    }
                    else
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidUntilDT", role.ValidUntilDT.ToString()),
                            ("newValidUntilDT", string.Empty) // empty string is a "none". Will be translated in the profile mapper.
                        });
                        role.ValidUntilDT = null;
                    }
                }
                else
                {
                    if (DateTime.TryParse(validUntilDT, out DateTime dt))
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidUntilDT", string.Empty),
                            ("newValidUntilDT", DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToString())
                        });
                        role.ValidUntilDT = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    }
                    else
                    {
                        changes.AddRange(new List<(string key, string value)>()
                        {
                            ("oldValidUntilDT", string.Empty),
                            ("newValidUntilDT", "error") // TODO: should be handled as a user input error?
                        });
                        role.ValidUntilDT = null;
                    }
                }
                _logger.LogInformation($"RoleID={role.ID} > Modified valid UNTIL date");
            }
            return changes;
        }

        public async Task ModifyRolePermissions(CTAMRole role, IEnumerable<int> addPermissionIDs, IEnumerable<int> removePermissionIDs)
        {
            if (addPermissionIDs != null && addPermissionIDs.Any())
            {
                _logger.LogInformation($"RoleID={role.ID} > Adding permissions: {string.Join(", ", addPermissionIDs)}");
                var permissionsToAdd = addPermissionIDs.Select(permissionID => new CTAMRole_Permission()
                {
                    CTAMRoleID = role.ID,
                    CTAMPermissionID = permissionID,
                });
                await _context.CTAMRole_Permission().AddRangeAsync(permissionsToAdd);
                var addedPermissions = await _context.CTAMPermission().Where(p => addPermissionIDs.Contains(p.ID)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var permission in addedPermissions)
                {
                    logMessageList.Add($"{permission.Description}");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedPermissionsToRole), 
                    ("permissions", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
            if (removePermissionIDs != null && removePermissionIDs.Any())
            {
                _logger.LogInformation($"RoleID={role.ID} > Removing permissions: {string.Join(", ", removePermissionIDs)}");
                var permissionsToRemove = await _context.CTAMRole_Permission()
                    .Where(rp => role.ID.Equals(rp.CTAMRoleID) && removePermissionIDs.Contains(rp.CTAMPermissionID))
                    .ToArrayAsync();

                var readPermissionID = await _context.CTAMRole_Permission().Include(rp => rp.CTAMPermission).Where(rp => rp.CTAMPermission.Description.Equals("Read")).Select(rp => rp.CTAMPermissionID).FirstOrDefaultAsync();
                var writePermissionID = await _context.CTAMRole_Permission().Include(rp => rp.CTAMPermission).Where(rp => rp.CTAMPermission.Description.Equals("Write")).Select(rp => rp.CTAMPermissionID).FirstOrDefaultAsync();

                if (removePermissionIDs.Contains(readPermissionID) || removePermissionIDs.Contains(writePermissionID))
                {
                    var userRolesPermissions = await _context.CTAMUser_Role()
                    .Include(ur => ur.CTAMUser)
                    .Include(ur => ur.CTAMRole)
                        .ThenInclude(r => r.CTAMRole_Permission)
                        .ThenInclude(rp => rp.CTAMPermission)
                    .SelectMany(ur => ur.CTAMRole.CTAMRole_Permission.Where(rp => rp.CTAMPermissionID == readPermissionID || rp.CTAMPermissionID == writePermissionID), (ur, rp) => new { ur.CTAMUser, rp })
                    .Where(urp => !(urp.rp.CTAMRoleID == role.ID && removePermissionIDs.Contains(urp.rp.CTAMPermissionID)))
                    .ToListAsync();

                    var users = userRolesPermissions.Select(urp => urp.CTAMUser.UID).Distinct();
                    var user = users.Where(u => userRolesPermissions.Where(urp => urp.CTAMUser.UID.Equals(u) && urp.rp.CTAMPermissionID == readPermissionID).Any()
                        && userRolesPermissions.Where(urp => urp.CTAMUser.UID.Equals(u) && urp.rp.CTAMPermissionID == writePermissionID).Any()).FirstOrDefault();

                    if (user == null)
                    {
                        throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_lastPermissionWithReadAndWriteAccess);
                    }
                }
                _context.CTAMRole_Permission().RemoveRange(permissionsToRemove);
                var removedPermissions = await _context.CTAMPermission().Where(p => removePermissionIDs.Contains(p.ID)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var permission in removedPermissions)
                {
                    logMessageList.Add($"{permission.Description}");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedPermissionsFromRole), 
                    ("permissions", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
        }

        public async Task ModifyRoleUsers(CTAMRole role, IEnumerable<string> addUserUIDs, IEnumerable<string> removeUserUIDs)
        {
            if (addUserUIDs != null && addUserUIDs.Any())
            {
                _logger.LogInformation($"RoleID='{role.ID}' > Adding users: '{string.Join(", ", addUserUIDs)}");
                var usersToAdd = addUserUIDs.Select(userUID => new CTAMUser_Role()
                {
                    CTAMRoleID = role.ID,
                    CTAMUserUID = userUID,
                });
                await _context.CTAMUser_Role().AddRangeAsync(usersToAdd);
                var addedUsers = await _context.CTAMUser().Where(u => addUserUIDs.Contains(u.UID)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var user in addedUsers)
                {
                    logMessageList.Add($"'{user.Name}, {user.Email}'");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_usersAddedToRole), 
                    ("users", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
            if (removeUserUIDs != null && removeUserUIDs.Any())
            {
                _logger.LogInformation($"RoleID='{role.ID}' > Removing users: '{string.Join(", ", removeUserUIDs)}");
                var usersToRemove = await _context.CTAMUser_Role()
                    .Where(ur => role.ID.Equals(ur.CTAMRoleID) && removeUserUIDs.Contains(ur.CTAMUserUID))
                    .ToArrayAsync();

                if (addUserUIDs == null || !addUserUIDs.Any())
                {
                    var anyUsersWithReadAndWriteAccess = await _context.CTAMUser_Role()
                    .Include(ur => ur.CTAMRole)
                        .ThenInclude(r => r.CTAMRole_Permission)
                        .ThenInclude(rp => rp.CTAMPermission)
                    .Where(ur => (ur.CTAMRoleID != role.ID || (ur.CTAMRoleID == role.ID && !removeUserUIDs.Contains(ur.CTAMUserUID)))
                        && ur.CTAMRole.CTAMRole_Permission.Where(rp => rp.CTAMPermission.Description.Equals("Read")).Any()
                        && ur.CTAMRole.CTAMRole_Permission.Where(rp => rp.CTAMPermission.Description.Equals("Write")).Any()).ToListAsync();
                    if (!anyUsersWithReadAndWriteAccess.Any())
                    {
                        throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_lastUserWithReadAndWriteAccessRole);
                    }
                }
                _context.CTAMUser_Role().RemoveRange(usersToRemove);
                var removedUsers = await _context.CTAMUser().Where(u => removeUserUIDs.Contains(u.UID)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var user in removedUsers)
                {
                    logMessageList.Add($"'{user.Name}, {user.Email}'");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedUsersFromRole), 
                    ("users", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
        }

        public async Task ModifyItemTypesForRoleAndSave(CTAMRole role, IEnumerable<int> addItemTypeIDs, IEnumerable<int> removeItemTypeIDs,
                                                        Dictionary<string, int> maxQtyToPickPerItemTypeID)
        {
            _logger.LogInformation("ModifyItemTypesForRole called");

            await ModifyMaxQtyToPickForExistingRoleItemTypes(role, maxQtyToPickPerItemTypeID);
            await ModifyRoleItemTypes(role, addItemTypeIDs, removeItemTypeIDs, maxQtyToPickPerItemTypeID);

            await _context.SaveChangesAsync(); // Needed when finally missing CabinetStock items have to be added
        }

        private async Task ModifyMaxQtyToPickForExistingRoleItemTypes(CTAMRole role, Dictionary<string, int> maxQtyToPickPerItemTypeID)
        {
            if (maxQtyToPickPerItemTypeID != null && maxQtyToPickPerItemTypeID.Count > 0)
            {
                var itemTypesIDs = maxQtyToPickPerItemTypeID.Keys.Select(maxQty => int.Parse(maxQty)).ToArray();
                _logger.LogInformation($"Modifying MaxQtyToPick for the following itemTypes: {itemTypesIDs}");

                Dictionary<string, int> oldMaxQtyToPickPerItemTypeID = new();
                await _context.CTAMRole_ItemType()
                    .Where(ri => role.ID == ri.CTAMRoleID && itemTypesIDs.Contains(ri.ItemTypeID))
                    .ForEachAsync(ri =>
                    {
                        oldMaxQtyToPickPerItemTypeID.Add(ri.ItemTypeID.ToString(), ri.MaxQtyToPick);
                        ri.MaxQtyToPick = maxQtyToPickPerItemTypeID[ri.ItemTypeID.ToString()];
                    });

                var itemTypes = await _context.ItemType().AsNoTracking().Where(it => itemTypesIDs.Contains(it.ID)).ToListAsync();
                List<string> logMessageList = new();
                foreach (var itemType in itemTypes)
                {
                    if (oldMaxQtyToPickPerItemTypeID.ContainsKey(itemType.ID.ToString()))
                    {
                        logMessageList.Add($"'{itemType.Description}: van {oldMaxQtyToPickPerItemTypeID[itemType.ID.ToString()]} naar {maxQtyToPickPerItemTypeID[itemType.ID.ToString()]}'");
                    }
                    
                    else
                    {
                        logMessageList.Add($"'{itemType.Description}: {maxQtyToPickPerItemTypeID[itemType.ID.ToString()]}'");
                    }
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_editedMaxQTYOfItemTypeOfRole), 
                    ("description", role.Description),
                    ("itemTypeQty", string.Join(", ", logMessageList)));
            }
        }

        private async Task ModifyRoleItemTypes(CTAMRole role, IEnumerable<int> addItemTypeIDs, IEnumerable<int> removeItemTypeIDs, Dictionary<string, int> maxQtyToPickPerItemTypeID)
        {
            if (addItemTypeIDs != null && addItemTypeIDs.Any())
            {
                _logger.LogInformation($"RoleID={role.ID} > Adding item types: '{string.Join(", ", addItemTypeIDs)}");
                var itemTypesToAdd = addItemTypeIDs.Select(itemTypeID => new CTAMRole_ItemType()
                {
                    CTAMRoleID = role.ID,
                    ItemTypeID = itemTypeID,
                    MaxQtyToPick = maxQtyToPickPerItemTypeID != null && maxQtyToPickPerItemTypeID.ContainsKey(itemTypeID.ToString()) ?
                        maxQtyToPickPerItemTypeID[itemTypeID.ToString()] : 1
                });
                await _context.CTAMRole_ItemType().AddRangeAsync(itemTypesToAdd);
                var addedItemTypes = await _context.ItemType().Where(it => addItemTypeIDs.Contains(it.ID)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var itemType in addedItemTypes)
                {
                    logMessageList.Add($"'{itemType.Description}'");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedItemTypesToRole), 
                    ("itemTypes", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
            if (removeItemTypeIDs != null && removeItemTypeIDs.Any())
            {
                _logger.LogInformation($"RoleID={role.ID} > Removing item types: '{string.Join(", ", removeItemTypeIDs)}");
                var itemTypesToRemove = await _context.CTAMRole_ItemType()
                    .Where(ri => role.ID == ri.CTAMRoleID && removeItemTypeIDs.Contains(ri.ItemTypeID))
                    .ToArrayAsync();
                _context.CTAMRole_ItemType().RemoveRange(itemTypesToRemove);
                var removedItemTypes = await _context.ItemType().Where(it => removeItemTypeIDs.Contains(it.ID)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var itemType in removedItemTypes)
                {
                    logMessageList.Add($"'{itemType.Description}'");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedItemTypesFromRole), 
                    ("itemTypes", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
        }

        public async Task ModifyCabinetsForRoleAndSave(CTAMRole role, IEnumerable<string> addCabinetNumbers, IEnumerable<string> removeCabinetNumbers)
        {
            _logger.LogInformation("ModifyCabinetsForRole called");
            if (addCabinetNumbers != null && addCabinetNumbers.Any())
            {
                _logger.LogInformation($"RoleID={role.ID} > Adding cabinets: '{string.Join(", ", addCabinetNumbers)}");
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedRoleToCabinets), 
                    ("description", role.Description),
                    ("cabinets", string.Join(", ", addCabinetNumbers)));

                var cabinetsToAdd = addCabinetNumbers.Select(cabinetNumber => new CTAMRole_Cabinet()
                {
                    CTAMRoleID = role.ID,
                    CabinetNumber = cabinetNumber,
                });
                await _context.CTAMRole_Cabinet().AddRangeAsync(cabinetsToAdd);
                var addedCabinets = await _context.Cabinet().Where(c => addCabinetNumbers.Contains(c.CabinetNumber)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var cabinet in addedCabinets)
                {
                    logMessageList.Add($"'{cabinet.Name}'");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedCabinetsToRole), 
                    ("cabinets", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }
            if (removeCabinetNumbers != null && removeCabinetNumbers.Any())
            {
                _logger.LogInformation($"RoleID={role.ID} > Removing cabinets: '{string.Join(", ", removeCabinetNumbers)}");
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedRoleFromCabinets), 
                    ("description", role.Description),
                    ("cabinets", string.Join(", ", removeCabinetNumbers)));

                var cabinetsToRemove = await _context.CTAMRole_Cabinet()
                    .Where(rc => role.ID == rc.CTAMRoleID && removeCabinetNumbers.Contains(rc.CabinetNumber))
                    .ToArrayAsync();
                _context.CTAMRole_Cabinet().RemoveRange(cabinetsToRemove);
                var removedCabinets = await _context.Cabinet().Where(c => removeCabinetNumbers.Contains(c.CabinetNumber)).ToListAsync();
                List<string> logMessageList = new List<string>();
                foreach (var cabinet in removedCabinets)
                {
                    logMessageList.Add($"'{cabinet.Name}'");
                }
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedCabinetsFromRole), 
                    ("cabinets", string.Join(", ", logMessageList)),
                    ("description", role.Description));
            }

            await _context.SaveChangesAsync(); // Needed when finally missing CabinetStock items have to be added
        }

        public async Task ModifyRoleCabinetAccessIntervals(CTAMRole role, IEnumerable<CabinetAccessIntervalCreateDTO> addCabinetAccessIntervals, 
                                                       IEnumerable<int> removeCabinetAccessIntervals)
        {
            _logger.LogInformation("ModifyCabinetAccessIntervals called");
            if (addCabinetAccessIntervals != null && addCabinetAccessIntervals.Any())
            {
                var cabinetAccessIntervalsToAdd = addCabinetAccessIntervals
                    .Select(c => new CabinetAccessInterval
                    {
                        CTAMRoleID = role.ID,
                        StartWeekDayNr = c.StartWeekDayNr,
                        StartTime = c.StartTime,
                        EndWeekDayNr = c.EndWeekDayNr,
                        EndTime = c.EndTime,
                    });
                _context.CabinetAccessIntervals().AddRange(cabinetAccessIntervalsToAdd);

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedIntervalsToRole), 
                    ("description", role.Description));
            }

            if (removeCabinetAccessIntervals != null && removeCabinetAccessIntervals.Any())
            {
                var cabinetAccessIntervalsToRemove = await _context.CabinetAccessIntervals().Where(c => removeCabinetAccessIntervals.Contains(c.ID)).ToListAsync();
                _context.CabinetAccessIntervals().RemoveRange(cabinetAccessIntervalsToRemove);

                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_deletedIntervalsFromRole), 
                    ("description", role.Description));
            }
        }

        public async Task AddMissingCabinetStockAndSave(int roleID)
        {
            var allCabinetNumbers = await _context.CTAMRole_Cabinet().Where(rc => rc.CTAMRoleID == roleID).Select(rc => rc.CabinetNumber).ToListAsync();
            var allItemTypeIDs = await _context.CTAMRole_ItemType().Where(rit => rit.CTAMRoleID == roleID).Select(rit => rit.ItemTypeID).ToListAsync();

            var cartesian = allCabinetNumbers.SelectMany(cn => allItemTypeIDs.Select(it => new { CabinetNumber = cn, ItemTypeID = it }));
            var existingStocks = (from cart in cartesian
                                 join cs in _context.CabinetStock()
                                 on new { cart.CabinetNumber, cart.ItemTypeID } equals new { cs.CabinetNumber, cs.ItemTypeID }
                                 select cart);

            var newCabinetStock = new List<CabinetStock>();
            foreach (var cs in cartesian.Except(existingStocks))
            {
                newCabinetStock.Add(new CabinetStock()
                {
                    CabinetNumber = cs.CabinetNumber,
                    ItemTypeID = cs.ItemTypeID,
                    CreateDT = DateTime.UtcNow,
                    MinimalStock = 0,
                    ActualStock = 0,
                    Status = CabinetStockStatus.OK
                });
            }

            if (newCabinetStock.Count > 0)
            {
                await _context.CabinetStock().AddRangeAsync(newCabinetStock);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task<CTAMRole> CreateRole(string description, string validFromDT, string validUntilDT, IEnumerable<int> permissionIDs, IEnumerable<string> userUIDs,
                                               IEnumerable<int> addItemTypeIDs, Dictionary<string, int> maxQtyToPickPerItemTypeID,
                                               IEnumerable<string> addCabinetNumbers, IEnumerable<CabinetAccessIntervalCreateDTO> addCabinetAccessIntervals)
        {
            if (permissionIDs == null || !permissionIDs.Any())
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_atLeastOnePermission);
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.roles_apiExceptions_emptyDescription);
            }
            var newRole = new CTAMRole();

            var changes = await ModifySingleRoleFields(newRole, description, validFromDT, validUntilDT);
            var role = _context.CTAMRole().Add(newRole).Entity;
            await _context.SaveChangesAsync();
            
            if (changes.Count > 0)
            {
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdRole), changes.ToArray());
            }

            await ModifyRolePermissions(role, permissionIDs, null);
            await ModifyRoleUsers(role, userUIDs, null);
            await ModifyItemTypesForRoleAndSave(role, addItemTypeIDs, null, maxQtyToPickPerItemTypeID);
            await ModifyCabinetsForRoleAndSave(role, addCabinetNumbers, null);
            await ModifyRoleCabinetAccessIntervals(role, addCabinetAccessIntervals, null);
            await AddMissingCabinetStockAndSave(role.ID);

            role = await _context.CTAMRole().AsNoTracking().Where(r => r.ID == role.ID)
                .Include(r => r.CTAMRole_Permission)
                    .ThenInclude(rp => rp.CTAMPermission)
                .FirstOrDefaultAsync();

            return role;
        }

        #endregion Roles
    }
}
