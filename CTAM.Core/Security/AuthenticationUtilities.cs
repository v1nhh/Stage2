using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CTAM.Core.Security
{
    public class AuthenticationUtilities
    {
        private readonly IConfiguration _configuration;

        public AuthenticationUtilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWTToken(IEnumerable<Claim> claims, int accessTokenValidityInSeconds = 0)
        {
            var privateKey = Convert.FromBase64String(_configuration.GetValue<string>("Jwt:PrivateKey"));

            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);


            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: accessTokenValidityInSeconds == 0 
                    ? DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:AccessTokenValidityInSeconds", JwtToken.AccessTokenValidityInSeconds))
                    : DateTime.UtcNow.AddSeconds(accessTokenValidityInSeconds),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateTokenFromEmail(string email, string tenantId)
        {
            var privateKey = Convert.FromBase64String(_configuration.GetValue<string>("Jwt:PrivateKey"));
            using RSA rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(privateKey, out _);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(CTAMClaimNames.TenandID, tenantId)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken GetJwtTokenFromString(string token, RsaSecurityKey rsa)
        {
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = rsa,
                ClockSkew = TimeSpan.Zero
            };
            handler.ValidateToken(token, validations, out var tokenSecure);
            var jwtToken = tokenSecure as JwtSecurityToken;
            return jwtToken;
        }

        public JwtSecurityToken ValidateJwtTokenOnlyOnSigningKey(string token, RsaSecurityKey rsa)
        {
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = rsa,
                ClockSkew = TimeSpan.Zero
            };

            handler.ValidateToken(token, validations, out var validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new ArgumentNullException(nameof(jwtToken));
            }
            return jwtToken;
        }

        public string ExtractClaimFromToken(string token, string expectedTenantId, string claimType, RsaSecurityKey rsa)
        {
            var jwtToken = GetJwtTokenFromString(token, rsa);

            var tenantIdClaim = jwtToken.Claims.Where(claim => claim.Type.Equals(CTAMClaimNames.TenandID)).FirstOrDefault();
            if (tenantIdClaim == null || string.IsNullOrWhiteSpace(tenantIdClaim.Value) || !tenantIdClaim.Value.Equals(expectedTenantId))
            {
                var msg = "Wrong TenantID. TenantID cannot be null or empty or unequal to one in header!";
                throw new ArgumentException(msg);
            }

            var claim = jwtToken.Claims.Where(claim => claim.Type.Equals(claimType)).FirstOrDefault();
            if (claim == null || string.IsNullOrWhiteSpace(claim.Value))
            {
                var msg = $"Claim of type '{claimType}' from token cannot be null or empty!";
                throw new ArgumentNullException(msg);
            }
            return claim.Value;
        }

        public string ExtractTenantIDFromToken(string token, RsaSecurityKey rsa)
        {
            var jwtToken = GetJwtTokenFromString(token, rsa);
            return jwtToken.Claims.Where(claim => claim.Type.Equals(CTAMClaimNames.TenandID)).FirstOrDefault().Value;
        }

        public string ExtractTenantIDFromClaims(HttpContext httpContext, ITenantContext tenantContext)
        {
            var userIsLoggedInMultipleTenants = httpContext.User.Claims.Where(c => c.Type == CTAMClaimNames.TenandID).Count() > 1;
            if (userIsLoggedInMultipleTenants)
            {
                return tenantContext.TenantId;
            }
            // User is logged in on single tenant
            return httpContext.User.Claims.Where(c => c.Type == CTAMClaimNames.TenandID)?.FirstOrDefault()?.Value;
        }

        public string ExtractTenantIDFromHeader(HttpContext httpContext)
        {
            return httpContext.Request.Headers.Where(h => h.Key.ToLower().Equals("x-tenant-id")).FirstOrDefault().Value;
        }

        public string ExtractSubValueFromClaims(HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(claim => claim.Properties.Values.Contains(JwtRegisteredClaimNames.Sub))?.Value;
        }
    }
}
