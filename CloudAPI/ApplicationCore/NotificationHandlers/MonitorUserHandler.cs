using CloudAPI.Infrastructure;
using CommunicationModule.ApplicationCore.DataManagers;
using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CommunicationModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Constants;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CloudAPI.ApplicationCore.NotificationHandlers
{
    public class MonitorUserHandler : INotificationHandler<EntitiesChangedNotification>
    {
        private readonly ILogger<MonitorUserHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommunicationDataManager _communicationDataManager;
        private MailTemplate _welcomeCabinetLoginTemplate;
        private MailTemplate _welcomeWebAndCabinetLoginTemplate;
        private MailTemplate _welcomeWebLoginTemplate;

        public MonitorUserHandler(ILogger<MonitorUserHandler> logger, MainDbContext context, IManagementLogger managementLogger, IHttpContextAccessor httpContextAccessor, CommunicationDataManager communicationDataManager)
        {
            _logger = logger;
            _context = context;
            _managementLogger = managementLogger;
            _httpContextAccessor = httpContextAccessor;
            _communicationDataManager = communicationDataManager;
        }

        public async Task Handle(EntitiesChangedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MonitorUserHandler called");
            try
            {
                if (notification.EntitiesData.Count() == 0)
                {
                    return;
                }

                var receivedUsersAdded = notification.EntitiesData
                     .Where(entityData => entityData.Entity.GetType().Equals(typeof(CTAMUser)) &&
                                          (entityData.State == EntityState.Added ||
                                           (entityData.State == EntityState.Modified && string.IsNullOrEmpty(((CTAMUser)entityData.OriginalEntity).LoginCode)
                                                                                     && !string.IsNullOrEmpty(((CTAMUser)entityData.Entity).LoginCode
                                          )))).ToList().Select(ed => (CTAMUser)ed.Entity);

                if (receivedUsersAdded.Any())
                {
                    var mqs = new List<MailQueue>();
                    var minimalLength = EmailRequirements.GetPasswordMinimalLength(_context.CTAMSetting().SingleOrDefault(s => s.ParName == CTAMSettingKeys.PasswordPolicy)?.ParValue);

                    foreach (var user in receivedUsersAdded)
                    {
                        var mq = await createUserWelcomeMail(user, minimalLength.ToString());
                        if (mq != null)
                        {
                            mqs.Add(mq);
                        }
                    }
                    await _communicationDataManager.AddMailQueueEntriesBulkAsync(mqs);

                    await _communicationDataManager.SaveChangesAsync();
                    
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_createdWelcomeMail), ("amount", mqs.Count().ToString()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetMostInnerException().Message);
            }
        }

        private async Task<MailQueue> createUserWelcomeMail(CTAMUser user, string minimalLength)
        {
            try
            {
                var markupTemplateID = 1;
                MailTemplate templateRecord = null;

                if (!string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.LoginCode))
                {
                    if (_welcomeWebAndCabinetLoginTemplate == null)
                    {
                        _welcomeWebAndCabinetLoginTemplate = await _communicationDataManager.GetMailTemplateByName(DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), user.LanguageCode);
                    }
                    templateRecord = _welcomeWebAndCabinetLoginTemplate;
                }
                else if (!string.IsNullOrEmpty(user.LoginCode))
                {
                    if (_welcomeCabinetLoginTemplate == null)
                    {
                        _welcomeCabinetLoginTemplate = await _communicationDataManager.GetMailTemplateByName(DefaultEmailTemplate.WelcomeCabinetLogin.GetName(), user.LanguageCode);
                    }
                    templateRecord = _welcomeCabinetLoginTemplate;
                }
                else if (!string.IsNullOrEmpty(user.Password))
                {
                    if (_welcomeWebLoginTemplate == null)
                    {
                        _welcomeWebLoginTemplate = await _communicationDataManager.GetMailTemplateByName(DefaultEmailTemplate.WelcomeWebLogin.GetName(), user.LanguageCode);
                    }
                    templateRecord = _welcomeWebLoginTemplate;
                }

                if (templateRecord != null)
                {
                    var link = "";
                    if (true == _httpContextAccessor?.HttpContext?.Request?.Headers?.TryGetValue("Origin", out var origin))
                    {
                        link = origin;
                    }
                    var parms = new Dictionary<string, string>()
                    {
                        { "name", user.Name }, // For backwards compatibility, might be removed if no occcurences of {{name}} in templates left
                        { "userName", user.Name },
                        { "email", user.Email },
                        { "password", user.Password },
                        { "loginCode", user.LoginCode },
                        { "pinCode", user.PinCode },
                        { "link", link },
                        { "minimalLength", minimalLength },
                    };

                    return new MailQueue()
                    {
                        MailMarkupTemplateID = markupTemplateID,
                        MailTo = user.Email,
                        Subject = templateRecord.Subject.FillTemplateWithDictionaryValues(templateRecord.Name, parms),
                        Body = templateRecord.Template.FillTemplateWithDictionaryValues(templateRecord.Name, parms),
                        Status = MailQueueStatus.Created
                    };
                }
            }
            catch (Exception e)
            {
                await _managementLogger.LogError(nameof(CloudTranslations.managementLog_errorCreatedWelcomeMail), 
                    ("email", user.Email),
                    ("error", e.GetMostInnerException().Message));
                _logger.LogError($"Added user {user.Email} email has not been processed with the following exception: {e.GetMostInnerException().Message}");
            }

            return null;
        }
    }
}
