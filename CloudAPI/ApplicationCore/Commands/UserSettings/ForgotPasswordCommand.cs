using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using CTAM.Core.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace CloudAPI.ApplicationCore.Commands.UserSettings
{
    public class ForgotPasswordCommand: IRequest
    {
        public string Email { get; set; }

        public int MailMarkupTemplateID { get; set; }

        [JsonIgnore]
        public string Origin { get; set; }

        [JsonIgnore]
        public string TenantID { get; set; }
    }

    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly ILogger<ForgotPasswordHandler> _logger;
        private readonly IMediator _mediator;
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IManagementLogger _managementLogger;

        public ForgotPasswordHandler(ILogger<ForgotPasswordHandler> logger, IMediator mediator, MainDbContext context, IConfiguration configuration, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _configuration = configuration;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ForgotPasswordHandler called");
            var user = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFoundWithEmail,
                                          new Dictionary<string, string> { { "email", request.Email } });
            }

            if (!user.Roles.Any(r => r.Permissions.Any(p => p.CTAMModule == CTAMModule.Management)))
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_passwordResetNoManagementPermission);
            }

            var authUtils = new AuthenticationUtilities(_configuration);
            var token = authUtils.GenerateTokenFromEmail(request.Email, request.TenantID);
            var minimalLength = EmailRequirements.GetPasswordMinimalLength(_context.CTAMSetting().SingleOrDefault(s => s.ParName == CTAMSettingKeys.PasswordPolicy)?.ParValue);

            _logger.LogInformation($"Token to change forgotten password generated for user with email {user.UID}");
            var link = $"{request.Origin}/password/{token}&{minimalLength}";
            var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
            {
                MailTemplateName = DefaultEmailTemplate.ForgotPassword.GetName(),
                LanguageCode = user.LanguageCode,
                MailMarkupTemplateID = request.MailMarkupTemplateID,
                MailTo = user.Email,
                EmailValues = new Dictionary<string, string>()
                {
                    { "name", user.Name },
                    { "link", link },
                    { "minimalLength", minimalLength.ToString() }
                },
            });

            if(emailIsSent) { 
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailResettedPasswordSentToUser),
                        ("name", user.Name),
                        ("email", user.Email));
            }
            return new Unit();
        }
    }

}
