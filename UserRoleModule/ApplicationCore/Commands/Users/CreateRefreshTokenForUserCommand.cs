using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.Web.Security;

namespace UserRoleModule.ApplicationCore.Commands.Users
{
    public class CreateRefreshTokenForUserCommand : IRequest<string>
    {
        public UserDTO User { get; set; }
    }

    public class CreateRefreshTokenForUserHandler : IRequestHandler<CreateRefreshTokenForUserCommand, string>
    {
        private readonly MainDbContext _context;
        private readonly UserAuthenticationService _userAuthService;
        private readonly IConfiguration _configuration;

        public CreateRefreshTokenForUserHandler(MainDbContext context, UserAuthenticationService userAuthService, IConfiguration configuration)
        {
            _context = context;
            _userAuthService = userAuthService;
            _configuration = configuration;
        }

        public async Task<string> Handle(CreateRefreshTokenForUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.CTAMUser().Where(x => x.Email == request.User.Email).FirstOrDefaultAsync();
            user.RefreshToken = _userAuthService.GenerateRefreshToken();
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:RefreshTokenValidityInSeconds", JwtToken.RefreshTokenValidityInSeconds));
            await _context.SaveChangesAsync();
            return user.RefreshToken;
        }
    }
}
