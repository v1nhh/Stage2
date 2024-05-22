using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAM.Core.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace CloudAPI.ApplicationCore.Commands.UserSettings
{
    public class ChangeForgottenPasswordCommand : IRequest
    {
        public string Token { get; set; }

        public string Password { get; set; }

        public int MailMarkupTemplateID { get; set; }

        [JsonIgnore]
        public string Origin { get; set; }

        [JsonIgnore]
        public string TenantID { get; set; }
    }

    public class ChangeForgottenPasswordHandler : IRequestHandler<ChangeForgottenPasswordCommand>
    {
        private readonly ILogger<ChangeForgottenPasswordHandler> _logger;
        private readonly IMediator _mediator;
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly RsaSecurityKey _rsa;
        private readonly IManagementLogger _managementLogger;

        public ChangeForgottenPasswordHandler(ILogger<ChangeForgottenPasswordHandler> logger, IMediator mediator, MainDbContext context, IConfiguration configuration, RsaSecurityKey rsa, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _configuration = configuration;
            _rsa = rsa;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(ChangeForgottenPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ChangeForgottenPasswordHandler called");
            var authUtils = new AuthenticationUtilities(_configuration);
            var email = authUtils.ExtractClaimFromToken(request.Token, request.TenantID, JwtRegisteredClaimNames.Email, _rsa);
            _logger.LogInformation($"Successfully extracted email '{email}' from provided token");
            var user = await _mediator.Send(new GetUserByEmailQuery(email));
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFoundWithEmail,
                                          new Dictionary<string, string> { { "email", email } });
            }
            await _mediator.Send(new ChangeUserPasswordCommand() {
                CTAMUserUID = user.UID,
                MailMarkupTemplateID = request.MailMarkupTemplateID,
                Password = request.Password,
                Origin = request.Origin,
            });

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_resettedPasswordForUser),
                ("name", user.Name),
                ("email", user.Email));
            return new Unit();
        }
    }

}
