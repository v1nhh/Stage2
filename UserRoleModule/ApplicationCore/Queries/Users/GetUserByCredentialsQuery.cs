using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;
using System.Collections.Generic;
using System;
using CTAM.Core.Interfaces;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetUserByCredentialsQuery : IRequest<UserDTO>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class GetUserByCredentialsHandler : IRequestHandler<GetUserByCredentialsQuery, UserDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUserByCredentialsQuery> _logger;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;
        private readonly ITenantService _tenantService;

        public GetUserByCredentialsHandler(MainDbContext context, ILogger<GetUserByCredentialsQuery> logger, IMapper mapper, ITenantContext tenantContext, ITenantService tenantService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _tenantContext = tenantContext;
            _tenantService = tenantService;
        }

        public async Task<UserDTO> Handle(GetUserByCredentialsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserByCredentialsHandler called");
            UserDTO result = null;
            var user = await _context.CTAMUser()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                    .ThenInclude(userRoles => userRoles.CTAMRole_Permission)
                    .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .Where(user => user.Email.Equals(request.Email))
                .FirstOrDefaultAsync();
            if (user != null)
            {
                CheckLicense();

                if (!user.Password.Equals(request.Password))
                {
                    user.BadLoginAttempts++;
                    await _context.SaveChangesAsync();
                    throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_wrongCredentials);
                }
                else if (user.BadLoginAttempts < 10 && user.Password.Equals(request.Password))
                {
                    if (user.BadLoginAttempts > 0)
                    {
                        user.BadLoginAttempts = 0;
                        await _context.SaveChangesAsync();
                    }

                    result = new UserDTO()
                    {
                        UID = user.UID,
                        Email = user.Email,
                        Name = user.Name,
                        CardCode = user.CardCode,
                        PhoneNumber = user.PhoneNumber,
                        LanguageCode = user.LanguageCode,
                        Roles = user.CTAMUser_Roles.Select(ur => _mapper.Map<RoleDTO>(ur.CTAMRole)).ToList(),
                        IsPasswordTemporary = user.IsPasswordTemporary,
                    };
                }
            }

            return result;
        }

        private void CheckLicense()
        {
            var lic = _tenantService.GetLicenseFieldsForTenant(_tenantContext.TenantId);

            if (lic.StartLicense > DateTime.UtcNow.Date)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_licenseNotStarted,
                                          new Dictionary<string, string> { { "startDate", lic.StartLicense.Date.ToShortDateString() } });
            }
            else if (DateTime.UtcNow.Date > lic.EndLicense)
            {
                throw new CustomException(HttpStatusCode.Unauthorized, CloudTranslations.login_apiExceptions_licenseExpired);
            }

        }
    }

}
