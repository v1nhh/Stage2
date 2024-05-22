using AutoMapper;
using CloudAPI.Utilities;
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
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.UserSettings
{
    public class ResetUserPasswordCommand: IRequest
    {
        public string CTAMUserUID { get; set; }

        public int MailMarkupTemplateID { get; set; }
    }

    public class ResetUserPasswordHandler : IRequestHandler<ResetUserPasswordCommand>
    {
        private readonly ILogger<ResetUserPasswordHandler> _logger;
        private readonly IMediator _mediator;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;


        public ResetUserPasswordHandler(MainDbContext context, ILogger<ResetUserPasswordHandler> logger, IMediator mediator, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ResetUserPasswordHandler called");
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await GenerateAndSetTempPassword(request.CTAMUserUID);
                var minimalLength = EmailRequirements.GetPasswordMinimalLength(_context.CTAMSetting().SingleOrDefault(s => s.ParName == CTAMSettingKeys.PasswordPolicy)?.ParValue);

                var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.TemporaryPassword.GetName(),
                    LanguageCode = user.LanguageCode,
                    MailMarkupTemplateID = request.MailMarkupTemplateID,
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name },
                        { "password", user.Password },
                        { "minimalLength", minimalLength.ToString() },
                    },
                });

                if(emailIsSent) { 
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailNewPasswordSentToUser),
                        ("name", user.Name),
                        ("email", user.Email));
                }
                scope.Complete();
            }

            return new Unit();
        }

        private async Task<UserDTO> GenerateAndSetTempPassword(string uid)
        {
            var user = await _context.CTAMUser()
                .Include(user => user.CTAMUser_Roles)
                    .ThenInclude(userRoles => userRoles.CTAMRole)
                        .ThenInclude(role => role.CTAMRole_Permission)
                            .ThenInclude(rolePermission => rolePermission.CTAMPermission)
                .SingleOrDefaultAsync(ctamUser => ctamUser.UID == uid);
            if (user == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "uid", uid } });
            }

            if (!user.CTAMUser_Roles.Any(ur => ur.CTAMRole.CTAMRole_Permission.Any(rp => rp.CTAMPermission.CTAMModule == CTAMModule.Management)))
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_passwordResetNoManagementPermission);
            }

            var minimalLength = EmailRequirements.GetPasswordMinimalLength(_context.CTAMSetting().SingleOrDefault(s => s.ParName == CTAMSettingKeys.PasswordPolicy)?.ParValue);
            user.Password = PasswordUtilities.GenerateRandomPassword(minimalLength);

            user.IsPasswordTemporary = true;
            user.BadLoginAttempts = 0;
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New temporary password is successfully generated and stored for user '{uid}'");

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_generatedTemporaryPasswordForUser),
                    ("name", user.Name),
                    ("email", user.Email));

            return _mapper.Map<UserDTO>(user);
        }
    }

}
