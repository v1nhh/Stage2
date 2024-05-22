using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Enums;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.UserSettings
{
    public class ResetUserPincodeCommand: IRequest
    {
        public string CTAMUserUID { get; set; }

        public int MailMarkupTemplateID { get; set; }
    }

    public class ResetUserPincodeHandler : IRequestHandler<ResetUserPincodeCommand>
    {
        private readonly ILogger<ResetUserPincodeHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManagementLogger _managementLogger;

        public ResetUserPincodeHandler(MainDbContext context, ILogger<ResetUserPincodeHandler> logger, IMediator mediator, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(ResetUserPincodeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ResetUserPincodeHandler called");
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = await _context.CTAMUser().SingleOrDefaultAsync(ctamUser => ctamUser.UID == request.CTAMUserUID);
                if (user == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_notFound,
                                              new Dictionary<string, string> { { "uid", request.CTAMUserUID } });
                }

                if (string.IsNullOrEmpty(user.LoginCode))
                {
                    throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.users_apiExceptions_noLoginCode);
                }

                var random = new Random();
                user.PinCode = random.Next(0, 999999).ToString("000000");

                await _context.SaveChangesAsync();
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_generatedPincodeForUser),
                    ("name", user.Name),
                    ("email", user.Email));
                _logger.LogInformation($"New pincode is successfully generated and stored for user '{request.CTAMUserUID}'");


                var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.PincodeChanged.GetName(),
                    LanguageCode = user.LanguageCode,
                    MailMarkupTemplateID = request.MailMarkupTemplateID,
                    MailTo = user.Email,
                    EmailValues = new Dictionary<string, string>()
                    {
                        { "name", user.Name },
                        { "email", user.Email },
                        { "password", user.Password },
                        { "loginCode", user.LoginCode },
                        { "pinCode", user.PinCode }
                    },
                });

                if (emailIsSent)
                {
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailNewPincodeSentToUser),
                        ("name", user.Name),
                        ("email", user.Email));
                }
                scope.Complete();
            }

            return new Unit();
        }
    }

}
