using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace CloudAPI.ApplicationCore.Commands.UserSettings
{
    public class SendLoginDetailsCommand: IRequest
    {
        public string CTAMUserUID { get; set; }

        public string Language { get; set; }

        public int MailMarkupTemplateID { get; set; }

        [JsonIgnore]
        public string Origin { get; set; }

        [JsonIgnore]
        public string TenantID { get; set; }
    }

    public class SendLoginDetailsHandler : IRequestHandler<SendLoginDetailsCommand>
    {
        private readonly ILogger<SendLoginDetailsHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IManagementLogger _managementLogger;
        private readonly UserRoleDataManager _userRoleDataManager;

        public SendLoginDetailsHandler(ILogger<SendLoginDetailsHandler> logger, IMediator mediator, IManagementLogger managementLogger, UserRoleDataManager userRoleDataManager)
        {
            _logger = logger;
            _mediator = mediator;
            _managementLogger = managementLogger;
            _userRoleDataManager = userRoleDataManager;
        }

        public async Task<Unit> Handle(SendLoginDetailsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SendLoginDetailsHandler called");
            var user = await _mediator.Send(new GetUserByUidQuery(request.CTAMUserUID));
            if (!user.IsPasswordTemporary)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_sendDetailsWithNonTemporaryPassword);
            }

            var sendCabinetLoginValue = await _userRoleDataManager.GetCTAMSetting("should_send_cabinet_login", false);
            var minimalLength = await _userRoleDataManager.GetCTAMSetting(CTAMSettingKeys.PasswordPolicy, "20");

            var link = $"{request.Origin}";
            var emailIsSent = false;
            if (sendCabinetLoginValue) {
                emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(),
                    LanguageCode = request.Language,
                    MailMarkupTemplateID = request.MailMarkupTemplateID,
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name },
                        { "email", user.Email },
                        { "password", user.Password },
                        { "loginCode", user.LoginCode },
                        { "pinCode", user.PinCode },
                        { "link", link },
                        { "minimalLength", minimalLength }
                    }
                });
                    
            } else
            {
                emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.WelcomeWebLogin.GetName(),
                    LanguageCode = request.Language,
                    MailMarkupTemplateID = request.MailMarkupTemplateID,
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name },
                        { "email", user.Email },
                        { "password", user.Password },
                        { "link", link },
                        { "minimalLength", minimalLength },
                    }
                });
            }

            if(emailIsSent) { 
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailCredentialsSentToUser),
                        ("name", user.Name),
                        ("email", user.Email));
            }
            return new Unit();
        }
    }

}
