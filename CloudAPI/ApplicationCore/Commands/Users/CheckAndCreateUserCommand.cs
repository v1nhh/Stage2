using AutoMapper;
using CloudAPI.Utilities;
using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.Users
{
    public class CheckAndCreateUserCommand : IRequest<UserWebDTO>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string LoginCode { get; set; }

        public string CardCode { get; set; }

        public string PhoneNumber { get; set; }

        public string LanguageCode { get; set; }

        public List<int> AddRolesIDs { get; set; }

        public int MailMarkupTemplateID { get; set; }

        public bool AssignPassword { get; set; }
    }

    public class CheckAndCreateUserHandler : IRequestHandler<CheckAndCreateUserCommand, UserWebDTO>
    {
        private readonly ILogger<CheckAndCreateUserHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        private readonly Random _random = new();

        private const int MAX_LOGIN_CODE = 999999;
        private const int MAX_CREATE_ATTEMPTS = 5;

        public CheckAndCreateUserHandler(MainDbContext context, ILogger<CheckAndCreateUserHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<UserWebDTO> Handle(CheckAndCreateUserCommand request, CancellationToken cancellationToken)
        {
            UserDTO user = null;
            user = await CreateUserTransaction(request, request.LoginCode);

            return _mapper.Map<UserWebDTO>(user);
        }

        public async Task<UserDTO> CreateUserTransaction(CheckAndCreateUserCommand request, string loginCode)
        {
            UserDTO user;
            using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
            {
                user = await CreateUser(request, loginCode);
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdUser),
                ("name", user.Name), 
                ("email", user.Email));
                scope.Complete();
            }
            return user;
        }
        public async Task<UserDTO> CreateUser(CheckAndCreateUserCommand request, string loginCode)
        {
            var uid = Guid.NewGuid().ToString();
            var pinCode = _random.Next(0, 999999).ToString("000000");
            var minimalLength = EmailRequirements.GetPasswordMinimalLength(_context.CTAMSetting().SingleOrDefault(s => s.ParName == CTAMSettingKeys.PasswordPolicy)?.ParValue);
            var password = PasswordUtilities.GenerateRandomPassword(minimalLength);

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_emptyName);
            }

            if (!string.IsNullOrEmpty(loginCode) && loginCode.Length != 6)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_invalidLoginCode,
                                          new Dictionary<string, string> { { "loginCode", loginCode }, { "loginCodeSize", "6" } });
            }

            if (!string.IsNullOrEmpty(loginCode) && await _context.CTAMUser().AnyAsync(u => u.LoginCode == loginCode))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_duplicateLoginCode,
                                          new Dictionary<string, string> { { "loginCode", loginCode } });
            }

            if (!string.IsNullOrEmpty(request.CardCode) && await _context.CTAMUser().AnyAsync(u => u.CardCode == request.CardCode))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_duplicateCardCode,
                                          new Dictionary<string, string> { { "cardCode", request.CardCode } });
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_emptyEmail);
            }

            if (await _context.CTAMUser().AnyAsync(u => u.Email == request.Email))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_duplicateEmail,
                                          new Dictionary<string, string> { { "email", request.Email } });
            }

            var newUser = new CTAMUser()
            {
                UID = uid,
                Name = request.Name,
                LoginCode = string.IsNullOrEmpty(loginCode) ? null : loginCode,
                PinCode = pinCode,
                Email = request.Email,
                Password = request.AssignPassword ? password : null,
                CardCode = request.CardCode,
                PhoneNumber = request.PhoneNumber,
                LanguageCode = request.LanguageCode,
                IsPasswordTemporary = true,
            };
            var user = _context.CTAMUser().Add(newUser).Entity;

            await AddRoles(user, request.AddRolesIDs);

            await _context.SaveChangesAsync();

            user = await _context.CTAMUser()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                    .ThenInclude(userRoles => userRoles.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .SingleOrDefaultAsync(ctamUser => ctamUser.UID.Equals(uid));
            return _mapper.Map<UserDTO>(user);
        }

        private async Task AddRoles(CTAMUser user, List<int> rolesIDs)
        {
            if (rolesIDs != null && rolesIDs.Count != 0)
            {
                var rolesToAdd = rolesIDs.Select(roleId => new CTAMUser_Role()
                {
                    CTAMRoleID = roleId,
                    CTAMUserUID = user.UID,
                }).ToArray();

                await _context.CTAMUser_Role().AddRangeAsync(rolesToAdd);

                var role_descriptions = _context.CTAMRole().AsNoTracking().Where(r => rolesIDs.Contains(r.ID)).Select(r => r.Description).ToArray();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_addedRolesToUser),
                ("roles", String.Join(" - ", role_descriptions)),
                ("name", user.Name), 
                ("email", user.Email));
            }
        }
        public async Task<IEnumerable<string>> GetAvailableLoginCodes()
        {
            var lastLoginCodeInUsage = await _context.CTAMUser().AsNoTracking()
                                                .OrderBy(user => Convert.ToInt32(user.LoginCode))
                                                .Select(user => user.LoginCode)
                                                .LastOrDefaultAsync();
            int lastCodeInUse = Convert.ToInt32(lastLoginCodeInUsage);

            List<int> loginCodesNotInUse = new List<int>();
            if (lastCodeInUse < MAX_LOGIN_CODE)
            {
                loginCodesNotInUse = Enumerable.Range(lastCodeInUse + 1, Math.Min(MAX_LOGIN_CODE - lastCodeInUse, MAX_CREATE_ATTEMPTS)).ToList();
            }

            if (loginCodesNotInUse.Count < MAX_CREATE_ATTEMPTS)
            {
                int take = MAX_CREATE_ATTEMPTS - loginCodesNotInUse.Count;
                var loginCodesInUsage = _context.CTAMUser().AsNoTracking()
                                                .OrderBy(user => Convert.ToInt32(user.LoginCode))
                                                .Select(user => Convert.ToInt32(user.LoginCode));

                var a = Enumerable.Range(1, MAX_LOGIN_CODE);

                var loginCodesNotInUseFromGaps = a.Except(loginCodesInUsage).Take(take).ToList();
                loginCodesNotInUse.AddRange(loginCodesNotInUseFromGaps);
            }

            return loginCodesNotInUse.Select(i => i.ToString("000000"));
        }
    }
}
