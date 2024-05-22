using AutoMapper;
using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Exceptions;
using CTAM.Core.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CTAMSharedLibrary.Resources;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;

namespace UserRoleModule.ApplicationCore.Queries.Users
{
    public class GetUserByTokenQuery : IRequest<UserDTO>
    {
        public string Tenant { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
    }

    public class GetUserByTokenHandler : IRequestHandler<GetUserByTokenQuery, UserDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetUserByTokenQuery> _logger;
        private readonly IMapper _mapper;
        private readonly RsaSecurityKey _rsaSecurityKey;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationUtilities _authUtils;

        public GetUserByTokenHandler(MainDbContext context, ILogger<GetUserByTokenQuery> logger, RsaSecurityKey rsaSecurityKey, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _rsaSecurityKey = rsaSecurityKey;
            _configuration = configuration;
            _authUtils = new AuthenticationUtilities(_configuration);
        }

        public async Task<UserDTO> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserByTokenHandler called");

            if (request.TokenType.Equals(JwtToken.RefreshToken))
            {
                var user = await _context.CTAMUser().AsNoTracking()
                    .Include(user => user.CTAMUser_Roles)
                        .ThenInclude(userRoles => userRoles.CTAMRole)
                        .ThenInclude(role => role.CTAMRole_Permission)
                        .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                    .Where(user => user.RefreshToken.Equals(request.Token))
                    .FirstOrDefaultAsync();

                if(user == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_UserRefreshTokenNotFound);
                }

                if(DateTime.UtcNow > user.RefreshTokenExpiryDate)
                {
                    throw new CustomException(HttpStatusCode.Forbidden, CloudTranslations.roles_apiExceptions_UserRefreshTokenExpired);
                }

                var userDTO = new UserDTO()
                {
                    UID = user.UID,
                    Email = user.Email,
                    Roles = user.CTAMUser_Roles.Select(ur => _mapper.Map<RoleDTO>(ur.CTAMRole)).ToList()
                };

                return userDTO;

            } else if (request.TokenType.Equals(JwtToken.AccessToken))
            {
                var userEmail = _authUtils.ExtractClaimFromToken(request.Token, request.Tenant, JwtRegisteredClaimNames.Sub, _rsaSecurityKey);
                var user = await _context.CTAMUser()
                    .Include(user => user.CTAMUser_Roles)
                        .ThenInclude(userRoles => userRoles.CTAMRole)
                        .ThenInclude(userRoles => userRoles.CTAMRole_Permission)
                        .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                    .Where(user => user.Email.Equals(userEmail))
                    .FirstOrDefaultAsync();

                if(user == null)
                {
                    throw new Exception("User not found.");
                }

                return new UserDTO()
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
            else
            {
                throw new Exception("Not matching any valid token type.");
            }
        }
    }
}
