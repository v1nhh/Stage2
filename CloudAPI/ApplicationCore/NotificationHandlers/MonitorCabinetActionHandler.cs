using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CloudAPI.ApplicationCore.Commands;
using CloudAPI.Infrastructure;
using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Constants;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using ItemModule.ApplicationCore.Entities;
using LocalAPI.ApplicationCore.Enums;
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
    public class MonitorCabinetActionHandler : INotificationHandler<EntitiesChangedNotification>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MonitorCabinetActionHandler> _logger;
        private readonly IManagementLogger _managementLogger;
        private readonly MainDbContext _context;
        private readonly UserRoleDataManager _userRoleDataManager;

        public MonitorCabinetActionHandler(ILogger<MonitorCabinetActionHandler> logger, IMediator mediator, IManagementLogger managementLogger, MainDbContext context, UserRoleDataManager userRoleDataManager)
        {
            _logger = logger;
            _mediator = mediator;
            _managementLogger = managementLogger;
            _context = context;
            _userRoleDataManager = userRoleDataManager;
        }

        public async Task Handle(EntitiesChangedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MonitorCabinetActionHandler called");
            if (notification.EntitiesData.Count() == 0)
            {
                return;
            }

            var receivedCabinetActions = notification.EntitiesData
                 .Where(entityData => entityData.Entity.GetType().Equals(typeof(CabinetAction))).ToList().Select(ed => (CabinetAction)ed.Entity);

            foreach (CabinetAction cabinetAction in receivedCabinetActions)
            {
                await handleCabinetAction(cabinetAction);
            }
            return;
        }

        private async Task handleCabinetAction(CabinetAction cabinetAction)
        {
            var languageCode = await _userRoleDataManager.GetCTAMSetting(CTAMSettingKeys.EmailDefaultLanguage, "en-US");

            var putItem = await _context.Item().AsNoTracking()
                .Where(i => i.ID.Equals(cabinetAction.PutItemID))
                .Include(i => i.ItemType)
                .FirstOrDefaultAsync();
            var takeItem = await _context.Item().AsNoTracking()
                .Where(i => i.ID.Equals(cabinetAction.TakeItemID))
                .Include(i => i.ItemType)
                .FirstOrDefaultAsync();

            var cabinet = await _context.Cabinet().Where(c => c.CabinetNumber.Equals(cabinetAction.CabinetNumber)).FirstOrDefaultAsync();

            try
            {
                switch (cabinetAction.Action)
                {
                    case CabinetActionStatus.Swap:
                        await HandleSwap(cabinetAction, languageCode, putItem, takeItem, cabinet);
                        break;
                    case CabinetActionStatus.SwapBack:
                        await HandleSwapBack(cabinetAction, languageCode, putItem, takeItem, cabinet);
                        break;
                    case CabinetActionStatus.Return:
                        await HandleReturn(cabinetAction, languageCode, putItem, cabinet);
                        break;
                    case CabinetActionStatus.Repaired:
                        await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_repairItemAction),
                                ("cabinetNumber", cabinetAction.CabinetNumber),
                                ("name", cabinetAction.CabinetName),
                                ("putDescription", cabinetAction.PutItemDescription));

                        var referredRequest = await _context.Request().Include(r => r.APISetting)
                            .Where(r => r.Status == RequestStatus.Open
                                && r.APISetting.TriggerName.Equals(APITriggerName.SendOnRepaired)
                                && r.EntityID.ToString().Equals(cabinetAction.PutItemID.ToString())
                                && r.EntityType.Equals(typeof(Item).Name)).FirstOrDefaultAsync();

                        await _mediator.Send(new CreateRequestsCommand()
                        {
                            Context = cabinetAction,
                            APITriggerName = APITriggerName.SendOnRepaired,
                            EntityType = typeof(Item).Name,
                            EntityID = (int)cabinetAction.PutItemID,
                            ReferredRequestID = referredRequest != null ? referredRequest.ID : 0
                        });

                        break;
                    case CabinetActionStatus.Replace:
                        // TODO: add APISetting and relevant concrete request/response classes for for replace action.
                        await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_replaceItemAction),
                                ("cabinetNumber", cabinetAction.CabinetNumber),
                                ("name", cabinetAction.CabinetName),
                                ("putDescription", cabinetAction.PutItemDescription));
                        break;
                    default:
                        _logger.LogInformation($"No notification required for cabinet action: {cabinetAction.Action}");
                        break;
                }
            }
            catch (Exception e)
            {
                var cabinetActionSerialized = JsonConvert.SerializeObject(cabinetAction);
                await _managementLogger.LogError(nameof(CloudTranslations.managementLog_errorCabinetAction),
                                ("cabinetAction", cabinetActionSerialized),
                                ("error", e.GetMostInnerException().Message));
                _logger.LogInformation($"CabinetAction {cabinetActionSerialized} has not been processed with the following exception: {e.GetMostInnerException().Message}");

            }
        }

        private async Task HandleReturn(CabinetAction cabinetAction, string languageCode, Item putItem, Cabinet cabinet)
        {
            // For now the incorrect emails and logs are almost identical but they will be changed when there is more thought put into it
            if (cabinetAction.CorrectionStatus == CorrectionStatus.ClosedUIP)
            {
                var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
                    LanguageCode = languageCode,
                    MailTo = cabinet.Email,
                    EmailValues = new Dictionary<string, string>()
                            {
                                { "userEmail",  cabinetAction.CTAMUserEmail},
                                { "itemDescription", cabinetAction.PutItemDescription},
                                { "itemTypeDescription", putItem.ItemType.Description },
                                { "cabinetName", cabinetAction.CabinetName },
                                { "positionAlias", cabinetAction.PositionAlias}
                            }
                });

                if(emailIsSent)
                {
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailIncorrectReturnClosedUIPSent),
                        ("cabinetNumber", cabinetAction.CabinetNumber),
                        ("name", cabinetAction.CabinetName),
                        ("putDescription", cabinetAction.PutItemDescription),
                        ("email", cabinet.Email));
                }
            }
            else if (cabinetAction.CorrectionStatus == CorrectionStatus.CreatedUIP)
            {
                var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
                    LanguageCode = languageCode,
                    MailTo = cabinet.Email,
                    EmailValues = new Dictionary<string, string>()
                            {
                                { "userEmail",  cabinetAction.CTAMUserEmail},
                                { "itemDescription", cabinetAction.PutItemDescription},
                                { "itemTypeDescription", putItem.ItemType.Description },
                                { "cabinetName", cabinetAction.CabinetName },
                                { "positionAlias", cabinetAction.PositionAlias}
                            }
                });
                if (emailIsSent)
                {
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailIncorrectReturnCreatedUIPSent),
                        ("cabinetNumber", cabinetAction.CabinetNumber),
                        ("name", cabinetAction.CabinetName),
                        ("putDescription", cabinetAction.PutItemDescription),
                        ("email", cabinet.Email));
                }
            }
            else if (!string.IsNullOrEmpty(cabinetAction.ErrorCodeDescription))
            {
                var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
                {
                    MailTemplateName = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(),
                    LanguageCode = languageCode,
                    MailTo = cabinet.Email,
                    EmailValues = new Dictionary<string, string>()
                            {
                                { "userName",  cabinetAction.CTAMUserName},
                                { "itemDescription", cabinetAction.PutItemDescription},
                                { "itemTypeDescription", putItem.ItemType.Description },
                                { "errorCodeDescription", cabinetAction.ErrorCodeDescription?? "-"},
                                { "cabinetName", cabinetAction.CabinetName },
                                { "positionAlias", cabinetAction.PositionAlias}
                            }
                });
                if(emailIsSent)
                {
                    await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailReturnedItemSent),
                        ("cabinetNumber", cabinetAction.CabinetNumber),
                        ("name", cabinetAction.CabinetName),
                        ("putDescription", cabinetAction.PutItemDescription),
                        ("errorDescription", cabinetAction.ErrorCodeDescription ?? "-"),
                        ("email", cabinet.Email));
                }
            }
        }

        private async Task HandleSwap(CabinetAction cabinetAction, string languageCode, Item putItem, Item takeItem, Cabinet cabinet)
        {
            var emailValues = await GetEmailValuesCabinetActionDictionary(cabinetAction, languageCode, putItem, takeItem, cabinet);
            var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
            {
                MailTemplateName = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(),
                LanguageCode = languageCode,
                MailTo = cabinet.Email,
                EmailValues = emailValues
            });

            if(emailIsSent)
            {
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailSwapItemSent),
                    ("cabinetNumber", cabinetAction.CabinetNumber),
                    ("name", cabinetAction.CabinetName),
                    ("putDescription", cabinetAction.PutItemDescription),
                    ("errorDescription", cabinetAction.ErrorCodeDescription ?? "-"),
                    ("email", cabinet.Email));
            }

            await _mediator.Send(new CreateRequestsCommand()
            {
                Context = cabinetAction,
                APITriggerName = APITriggerName.SendOnSwap,
                EntityType = typeof(Item).Name,
                EntityID = (int)cabinetAction.PutItemID
            });
        }

        private async Task<Dictionary<string, string>> GetEmailValuesCabinetActionDictionary(CabinetAction cabinetAction, string languageCode, Item putItem, Item takeItem, Cabinet cabinet)
        {
            var errorCode = "-";

            if (!string.IsNullOrEmpty(cabinetAction.ErrorCodeDescription))
            {
                var ec = await _context.ErrorCode().Where(ec => ec.Description.Equals(cabinetAction.ErrorCodeDescription)).Select(ec => ec.Code).FirstOrDefaultAsync();
                if (ec != null)
                {
                    errorCode = ec;
                }
            }

            var cabinetDescription = await _context.Cabinet().Where(c => c.CabinetNumber.Equals(cabinetAction.CabinetNumber)).Select(c => c.Description).FirstOrDefaultAsync();

            return new Dictionary<string, string>()
                        {
                            {"userName",  cabinetAction.CTAMUserName},
                            {"putItemDescription", cabinetAction.PutItemDescription},
                            {"putItemTypeDescription", putItem.ItemType.Description },
                            {"putItemExternalReferenceID", string.IsNullOrEmpty(putItem.ExternalReferenceID) ? "-" : putItem.ExternalReferenceID },
                            {"errorCodeDescription", cabinetAction.ErrorCodeDescription ?? "-"},
                            {"errorCode", errorCode},
                            {"cabinetName", cabinetAction.CabinetName },
                            {"cabinetDescription", cabinetDescription },
                            {"cabinetLocationDescr", cabinet.LocationDescr },
                            {"takeItemDescription", cabinetAction.TakeItemDescription},
                            {"takeItemTypeDescription", takeItem.ItemType.Description },
                            {"takeItemExternalReferenceID", string.IsNullOrEmpty(takeItem.ExternalReferenceID) ? "-" : takeItem.ExternalReferenceID },
                            {"actionDT", cabinetAction.ActionDT.ToLocalDateTimeString() },
                            {"positionAlias", cabinetAction.PositionAlias}
                        };
        }

        private async Task HandleSwapBack(CabinetAction cabinetAction, string languageCode, Item putItem, Item takeItem, Cabinet cabinet)
        {
            var emailValues = await GetEmailValuesCabinetActionDictionary(cabinetAction, languageCode, putItem, takeItem, cabinet);
            var emailIsSent = await _mediator.Send(new SendEmailFromTemplateCommand()
            {
                MailTemplateName = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(),
                LanguageCode = languageCode,
                MailTo = cabinet.Email,
                EmailValues = emailValues
            });

            if(emailIsSent) { 
                await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailSwapBackItemSent),
                    ("cabinetNumber", cabinetAction.CabinetNumber),
                    ("name", cabinetAction.CabinetName),
                    ("putDescription", cabinetAction.PutItemDescription),
                    ("errorDescription", cabinetAction.ErrorCodeDescription ?? "-"),
                    ("email", cabinet.Email));
            }
        }
    }
}
