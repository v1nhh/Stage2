using CTAM.Core;
using CTAM.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserRoleModule.ApplicationCore.Commands.Users
{
    public class DeleteRefreshTokenForUserCommand : IRequest
    {
        public HttpContext HttpContext { get; set; }
        public string TenantFromHeader { get; set; }
    }

    public class DeleteRefreshTokenForUserHandler : IRequestHandler<DeleteRefreshTokenForUserCommand, Unit>
    {
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly AuthenticationUtilities _authUtils;
        private readonly ILogger<DeleteRefreshTokenForUserCommand> _logger;
        private readonly RsaSecurityKey _rsa;

        public DeleteRefreshTokenForUserHandler(MainDbContext context, IConfiguration configuration, ILogger<DeleteRefreshTokenForUserCommand> logger, RsaSecurityKey rsa)
        {
            _context = context;
            _configuration = configuration;
            _authUtils = new AuthenticationUtilities(_configuration);
            _logger = logger;
            _rsa = rsa;
        }

        public async Task<Unit> Handle(DeleteRefreshTokenForUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(nameof(DeleteRefreshTokenForUserHandler) + "called");

            var tenant = command.HttpContext.Request.Headers["X-Tenant-ID"].FirstOrDefault();
            if (string.IsNullOrEmpty(tenant))
            {
                throw new ArgumentNullException("Tenant header can not be empty.");
            }

            var accessToken = command.HttpContext.Request.Cookies["access_token_" + tenant];
            var refreshToken = command.HttpContext.Request.Cookies["refresh_token_" + tenant];

            var jwtToken = _authUtils.ValidateJwtTokenOnlyOnSigningKey(accessToken, _rsa);
            var userEmail = _authUtils.ExtractClaimFromToken(accessToken, tenant, JwtRegisteredClaimNames.Sub, _rsa);

            var user = await _context.CTAMUser().Where(u => u.Email.Equals(userEmail) && u.RefreshToken.Equals(refreshToken)).FirstOrDefaultAsync();

            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryDate = null;

                await _context.SaveChangesAsync();
            }

            return new Unit();
        }
    }
}
