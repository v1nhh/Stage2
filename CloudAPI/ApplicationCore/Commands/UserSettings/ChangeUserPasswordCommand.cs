using AutoMapper;
using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Interfaces;


namespace CloudAPI.ApplicationCore.Commands.UserSettings
{
    public class ChangeUserPasswordCommand : IRequest
    {
        public string CTAMUserUID { get; set; }

        public string Password { get; set; }

        public int MailMarkupTemplateID { get; set; }

        [JsonIgnore]
        public string Origin { get; set; }
        }

    public class ChangeUserPasswordHandler : IRequestHandler<ChangeUserPasswordCommand>
    {
        private readonly ILogger<ChangeUserPasswordHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IManagementLogger _managementLogger;

        public ChangeUserPasswordHandler(ILogger<ChangeUserPasswordHandler> logger, MainDbContext context, IMapper mapper, IMediator mediator, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ChangeUserPasswordHandler called");
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await SetNewPassword(request.CTAMUserUID, request.Password);
                var link = $"{request.Origin}";

                await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.PasswordChanged.GetName(),
                    LanguageCode = user.LanguageCode,
                    MailMarkupTemplateID = request.MailMarkupTemplateID,
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name },
                        { "link", link },
                    },
                });
                scope.Complete();
            }

            return new Unit();
        }
        public async Task<UserDTO> SetNewPassword( string uid, string password)
        {
            var user = await _context.CTAMUser().SingleOrDefaultAsync(ctamUser => ctamUser.UID == uid);
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "uid", uid } });
            }
            var (regex, length) = EmailRequirements.GetPasswordRegex(_context.CTAMSetting().SingleOrDefault(s => s.ParName == CTAMSettingKeys.PasswordPolicy)?.ParValue);
            if (Regex.IsMatch(password, regex))
            {
                user.Password = password;
                user.IsPasswordTemporary = false;
                user.BadLoginAttempts = 0;
                await _context.SaveChangesAsync();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_newPasswordForUser),
                    ("name", user.Name),
                    ("email", user.Email));
            }
            else
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.users_apiExceptions_passwordNotMatchRequirements,
                                          new Dictionary<string, string> { { "minimalLength", length.ToString() } });
            }
            return _mapper.Map<UserDTO>(user);
        }
    }

}
