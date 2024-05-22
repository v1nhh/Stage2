using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using UserRoleModule.ApplicationCore.DTO;

namespace UserRoleModule.Web.Security
{
    public class UserAuthenticationService
    {
        private readonly ILogger<UserAuthenticationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationUtilities _authUtils;

        public UserAuthenticationService(ILogger<UserAuthenticationService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _authUtils = new AuthenticationUtilities(_configuration);
        }

        public string GenerateWebToken(UserDTO userInfo, string tenantID)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(CTAMClaimNames.TenandID, tenantID),
                    new Claim(CTAMClaimNames.ClientType, Enum.GetName(typeof(ClientType), ClientType.Web)),
                };
                var claimsWithRoles = claims.Concat(userInfo.Roles.SelectMany(role => role.Permissions.Select((p) => new Claim(CTAMClaimNames.Role, p.FullName)))).Distinct();
                
                return _authUtils.GenerateJWTToken(claimsWithRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
