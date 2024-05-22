using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CloudAPI.Infrastructure;
using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.NotificationHandlers
{
    public class MonitorCabinetPositionHandler : INotificationHandler<EntitiesChangedNotification>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MonitorCabinetPositionHandler> _logger;
        private readonly IManagementLogger _managementLogger;
        private readonly MainDbContext _context;
        private readonly UserRoleDataManager _userRoleDataManager;

        public MonitorCabinetPositionHandler(ILogger<MonitorCabinetPositionHandler> logger, IMediator mediator, IManagementLogger managementLogger, MainDbContext context, UserRoleDataManager userRoleDataManager)
        {
            _logger = logger;
            _mediator = mediator;
            _managementLogger = managementLogger;
            _context = context;
            _userRoleDataManager = userRoleDataManager;
        }

        public async Task Handle(EntitiesChangedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MonitorCabinetPositionHandler called");
            if (!notification.EntitiesData.Any())
            {
                return;
            }

            var languageCode = await _userRoleDataManager.GetCTAMSetting(CTAMSettingKeys.EmailDefaultLanguage, "en-US");

            var receivedCabinetPositions = notification.EntitiesData
                .Where(entityData => entityData.Entity.GetType().Equals(typeof(CabinetPosition)))
                .Select(ed => (CabinetPosition)ed.Entity);

            foreach (CabinetPosition cabinetPosition in receivedCabinetPositions)
            {
                await HandleCabinetPositions(cabinetPosition, languageCode);
            }
            return;
        }

        private async Task HandleCabinetPositions(CabinetPosition cabinetPosition, string languageCode)
        {
            var cabinet = await _context.Cabinet().Where(c => c.CabinetNumber.Equals(cabinetPosition.CabinetNumber)).FirstOrDefaultAsync();

            try
            {
                if (cabinetPosition.Status == CabinetPositionStatus.UnknownContent)
                {
                    var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                    {
                        MailTemplateName = DefaultEmailTemplate.UnknownContentPosition.GetName(),
                        LanguageCode = languageCode,
                        MailTo = cabinet.Email,
                        EmailValues = new Dictionary<string, string>()
                                {
                                    { "cabinetName", cabinet.Name },
                                    { "positionAlias", cabinetPosition.PositionAlias },
                                    { "positionNumber", cabinetPosition.PositionNumber.ToString() }
                                }
                    });

                    if(emailIsSent)
                    {
                        await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailUnknownContentPositionSent),
                            ("cabinetNumber", cabinet.CabinetNumber),
                            ("name", cabinet.Name),
                            ("positionAlias", cabinetPosition.PositionAlias),
                            ("positionNumber", cabinetPosition.PositionNumber.ToString()),
                            ("email", cabinet.Email));
                    }
                } 
                else if (cabinetPosition.Status == CabinetPositionStatus.MissingContent)
                {
                    var cabinetPositionContent = await _context.CabinetPositionContent().Include(cpc => cpc.Item).Where(cpc => cpc.CabinetPosition.PositionNumber.Equals(cabinetPosition.PositionNumber)).FirstOrDefaultAsync();
                    var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                    {
                        MailTemplateName = DefaultEmailTemplate.MissingContentPosition.GetName(),
                        LanguageCode = languageCode,
                        MailTo = cabinet.Email,
                        EmailValues = new Dictionary<string, string>()
                                {
                                    { "cabinetName", cabinet.Name },
                                    { "positionAlias", cabinetPosition.PositionAlias },
                                    { "positionNumber", cabinetPosition.PositionNumber.ToString() },
                                    { "itemDescription", cabinetPositionContent.Item.Description },
                                }
                    });

                    if(emailIsSent)
                    {
                        await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailMissingContentPositionSent),
                            ("cabinetNumber", cabinet.CabinetNumber),
                            ("name", cabinet.Name),
                            ("positionAlias", cabinetPosition.PositionAlias),
                            ("positionNumber", cabinetPosition.PositionNumber.ToString()),
                            ("itemDescription", cabinetPositionContent.Item.Description),
                            ("email", cabinet.Email));
                    }
                }
            }
            catch (Exception e)
            {
                var cabinetPositionSerialized = JsonConvert.SerializeObject(cabinetPosition);
                await _managementLogger.LogError(nameof(CloudTranslations.managementLog_errorCabinetPosition),
                                ("cabinetPosition", cabinetPositionSerialized),
                                ("error", e.GetMostInnerException().Message));
                _logger.LogInformation($"CabinetPosition {cabinetPositionSerialized} has not been processed with the following exception: {e.GetMostInnerException().Message}");
            }
        }
    }
}
