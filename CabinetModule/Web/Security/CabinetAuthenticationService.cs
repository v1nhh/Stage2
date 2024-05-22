using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CabinetModule.Web.Security
{
    public class CabinetAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthenticationUtilities _authUtils;

        public CabinetAuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _authUtils = new AuthenticationUtilities(_configuration);
        }

        public string GenerateCabinetToken(CabinetAuthenticationRequest request, string tenantID)
        {
            if (request.ApiKey == _configuration.GetValue<string>("Jwt:CabinetAPIKey"))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.CabinetNumber),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(CTAMClaimNames.Role, "CABINET_ACTIONS"),
                    new Claim(CTAMClaimNames.TenandID, tenantID),
                    new Claim(CTAMClaimNames.ClientType, Enum.GetName(typeof(ClientType), ClientType.Cabinet)),
                };
                return _authUtils.GenerateJWTToken(claims, 7 * 24 * 60 * 60);
            }
            else
            {
                throw new UnauthorizedAccessException("apiKey is not valid");
            }
        }
    }
}
