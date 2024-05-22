using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Queries;
using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CloudAPI.ApplicationCore.Services;
using CloudAPI.Infrastructure;
using CTAM.Core.Enums;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemCabinetModule.ApplicationCore.Queries;
using ItemCabinetModule.ApplicationCore.Queries.Sync.LiveSync;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.NotificationHandlers
{
    public class MonitorEntitiesChangedHandler : INotificationHandler<EntitiesChangedNotification>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MonitorEntitiesChangedHandler> _logger;
        private readonly LiveSyncService _liveSyncService;

        public MonitorEntitiesChangedHandler(ILogger<MonitorEntitiesChangedHandler> logger, IMediator mediator, LiveSyncService liveSyncService)
        {
            _logger = logger;
            _mediator = mediator;
            _liveSyncService = liveSyncService;
        }

        public async Task Handle(EntitiesChangedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("LiveSyncNotificationHandler called");
            if (notification.EntitiesData.Count() == 0)
            {
                return;
            }
            // Collect main entities that are changed in a CollectedLiveSyncMessage to send with SignalR to the cabinets.
            // The cabinets then decide for which entities/envelopes they will send a request to the CloucApi controller.
            var collectedLiveSyncMessage = new CollectedLiveSyncMessage();

            var changedEntities = notification.EntitiesData.Where(ed => ed.State.GetChangeAction() != ChangeAction.None);

            await MessageFactory(changedEntities, collectedLiveSyncMessage);

            collectedLiveSyncMessage.CabinetNumbers = LiveSyncService.AssembleAllCabinetNumbers(collectedLiveSyncMessage);
            var message = new MessageWrapper("SyncCollected", collectedLiveSyncMessage);
            await _liveSyncService.CollectAndSendMessage(message);
        }

        public async Task MessageFactory(IEnumerable<ChangedEntity> changedEntities, CollectedLiveSyncMessage collectedLiveSyncMessage)
        {
            var entities = changedEntities.Select(ce => ce.Entity);

            var itemIDsFromItems = entities.OfType<Item>().Select(i => i.ID);
            var itemIDsFromCTAMUserPersonalItemItemID = entities.OfType<CTAMUserPersonalItem>().Select(upi => upi.ItemID);
            var itemIDsFromCTAMUserPersonalItemReplacementID = entities.OfType<CTAMUserPersonalItem>().Where(upi => upi.ReplacementItemID != null).Select(upi => upi.ReplacementItemID.Value);
            var itemIDsFromCTAMUserInPossessions = entities.OfType<CTAMUserInPossession>().Select(uip => uip.ItemID);

            var itemToItemType = await _mediator.Send(new GetItemTypeIDsByItemIDsQuery() { ItemIDs = itemIDsFromCTAMUserPersonalItemItemID.Union(itemIDsFromCTAMUserPersonalItemReplacementID).Union(itemIDsFromCTAMUserInPossessions).Union(itemIDsFromItems) });
            // Gather all ItemType.ID <-> CabinetNumber references
            var itemTypeIDsFromItems = entities.OfType<Item>().Select(i => i.ItemTypeID);
            var itemTypeIDsFromItemTypes = entities.OfType<ItemType>().Select(i => i.ID);
            var itemTypeIDsFromItemsItemTypesErrorCodes = entities.OfType<ItemType_ErrorCode>().Select(itec => itec.ItemTypeID);
            var itemTypeIDsFromCTAMUserInPossessionsAndCTAMUserPersonalItems = itemToItemType.Values.Distinct();
            var distinctItemTypeIDs = itemTypeIDsFromItems.Union(itemTypeIDsFromItemTypes).Union(itemTypeIDsFromItemsItemTypesErrorCodes).Union(itemTypeIDsFromCTAMUserInPossessionsAndCTAMUserPersonalItems);

            var cabinetNumbersByItemTypeID = new Dictionary<int, List<string>>();
            if (distinctItemTypeIDs.Any())
            {
                cabinetNumbersByItemTypeID = await _mediator.Send(new GetRelatedCabinetNumbersByItemTypeIDsQuery(distinctItemTypeIDs));
            }

            // Gather all User.UID <-> CabinetNumber references
            var uids = entities.OfType<CTAMUser>().Select(u => u.UID);
            var uidsFromCTAMUserInPossessions = entities.OfType<CTAMUserInPossession>().SelectMany(uip => new List<string> { uip.CTAMUserUIDOut, uip.CTAMUserUIDIn}).Where(uid => uid != null);
            var uidsFromCTAMUserPersonalItems = entities.OfType<CTAMUserPersonalItem>().Select(upi => upi.CTAMUserUID);
            var distinctUIDs = uids.Union(uidsFromCTAMUserInPossessions).Union(uidsFromCTAMUserPersonalItems);

            var cabinetNumbersByUserUID = new Dictionary<string, List<string>>();
            if (distinctUIDs.Any())
            {
                cabinetNumbersByUserUID = await _mediator.Send(new GetRelatedCabinetNumbersByUserUIDsQuery(distinctUIDs));
            }

            // Gather all Role.ID <-> CabinetNumber references
            var roleIDsFromUserRoles = entities.OfType<CTAMUser_Role>().Select(ur => ur.CTAMRoleID);
            var roleIDsFromRoles = entities.OfType<CTAMRole>().Select(r => r.ID);
            var roleIDsFromRolePermissions = entities.OfType<CTAMRole_Permission>().Select(rp => rp.CTAMRoleID);
            var roleIDsFromRoleItemTypes = entities.OfType<CTAMRole_ItemType>().Select(ri => ri.CTAMRoleID);
            var roleIDsFromCabinetAccessIntervals = entities.OfType<CabinetAccessInterval>().Select(cai => cai.CTAMRoleID);
            var distinctRoleIDs = roleIDsFromUserRoles.Union(roleIDsFromRoles).Union(roleIDsFromRolePermissions).Union(roleIDsFromRoleItemTypes).Union(roleIDsFromCabinetAccessIntervals);

            var cabinetNumbersByRoleID = new Dictionary<int, List<string>>();
            if (distinctRoleIDs.Any())
            {
                cabinetNumbersByRoleID = await _mediator.Send(new GetRelatedCabinetNumbersByRoleIDsQuery(distinctRoleIDs));
            }

            // Gather all ErrorCode.ID <-> CabinetNumber references
            var errorCodeIDsFromErrorCodes = entities.OfType<ErrorCode>().Select(e => e.ID);

            var cabinetNumbersByErrorCodeID = new Dictionary<int, List<string>>();
            if (errorCodeIDsFromErrorCodes.Any())
            {
                cabinetNumbersByErrorCodeID = await _mediator.Send(new GetRelatedCabinetNumbersByErrorCodeIDsQuery(errorCodeIDsFromErrorCodes));
            }

            foreach (ChangedEntity changedEntity in changedEntities)
            {
                var changeAction = changedEntity.State.GetChangeAction();

                switch (changedEntity.Entity)
                {
                    case Cabinet cabinet:
                        var cabMsg = new CabinetLiveSyncMessage() { CabinetNumber = cabinet.CabinetNumber, ChangeAction = changeAction };
                        collectedLiveSyncMessage.CabinetMessages.Add(cabMsg);
                        break;
                    case CabinetUI cabinetUI:
                        var cabUIMsg = new CabinetUILiveSyncMessage() { CabinetNumber = cabinetUI.CabinetNumber, ChangeAction = changeAction };
                        collectedLiveSyncMessage.CabinetUIMessages.Add(cabUIMsg);
                        break;
                    case CabinetDoor cabinetDoor:
                        var cabDoorMsg = new CabinetDoorLiveSyncMessage() { CabinetNumber = cabinetDoor.CabinetNumber, DoorID = cabinetDoor.ID, ChangeAction = changeAction };
                        collectedLiveSyncMessage.CabinetDoorMessages.Add(cabDoorMsg);
                        break;
                    case Item item:
                        var itemMsg = new ItemLiveSyncMessage()
                        {
                            ItemID = item.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByItemTypeID.ContainsKey(item.ItemTypeID) ? cabinetNumbersByItemTypeID[item.ItemTypeID] : new List<string>()
                        };
                        collectedLiveSyncMessage.ItemMessages.Add(itemMsg);
                        break;
                    case ItemType itemType:
                        var itemTypeMsg = new ItemTypeLiveSyncMessage()
                        {
                            ItemTypeID = itemType.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = changeAction == ChangeAction.Delete ? new List<string>()
                                                : (cabinetNumbersByItemTypeID.ContainsKey(itemType.ID) ? cabinetNumbersByItemTypeID[itemType.ID] : new List<string>())
                        };
                        collectedLiveSyncMessage.ItemTypeMessages.Add(itemTypeMsg);
                        break;
                    case CTAMUser user:
                        var originalUser = (CTAMUser) changedEntity.OriginalEntity;
                        // Lambda function to create a livesync message for an user.
                        Action createAndAddUserMsg = () =>
                        {
                            var userMsg = new UserLiveSyncMessage
                            {
                                UserUID = user.UID,
                                ChangeAction = changeAction,
                                CabinetNumbers = changeAction == ChangeAction.Delete ? new List<string>() :
                                                (cabinetNumbersByUserUID.ContainsKey(user.UID) ? cabinetNumbersByUserUID[user.UID] : new List<string>())
                            };
                            collectedLiveSyncMessage.UserMessages.Add(userMsg);
                        };
                        if (originalUser != null)
                        {
                            // Get the changed properties other than the properties RefreshToken, RefreshTokenExpiryDate and UpdateDT
                            var properties = typeof(CTAMUser).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.Name != nameof(CTAMUser.RefreshToken) && p.Name != nameof(CTAMUser.RefreshTokenExpiryDate) && p.Name != nameof(CTAMUser.UpdateDT));

                            // For each property check if there is a change. If there is a change for a property, a livesync message should be created for this user entity.
                            foreach (var property in properties)
                            {
                                var currentValue = property.GetValue(user);
                                var previousValue = property.GetValue(originalUser);
                                if (!Equals(currentValue, previousValue))
                                {
                                    // Execute the lambda function to create a livesync message for a changed user
                                    createAndAddUserMsg();
                                    break;
                                }
                            }
                            break;
                        }
                        // Execute the lambda function to create a livesync message for a new user
                        createAndAddUserMsg();
                        break;
                    case CTAMUser_Role userRole:
                        var userRoleMsg = new UserRoleLiveSyncMessage()
                        {
                            UserUID = userRole.CTAMUserUID,
                            RoleID = userRole.CTAMRoleID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByRoleID.ContainsKey(userRole.CTAMRoleID) ? cabinetNumbersByRoleID[userRole.CTAMRoleID] : new List<string>()
                        };
                        collectedLiveSyncMessage.UserRoleMessages.Add(userRoleMsg);
                        break;
                    case CTAMRole role:
                        var roleMsg = new RoleLiveSyncMessage()
                        {
                            RoleID = role.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = changeAction == ChangeAction.Delete ? new List<string>()
                                                : (cabinetNumbersByRoleID.ContainsKey(role.ID) ? cabinetNumbersByRoleID[role.ID] : new List<string>())
                        };
                        collectedLiveSyncMessage.RoleMessages.Add(roleMsg);
                        break;
                    case CTAMRole_Permission rolePermission:
                        var rolePermissionMsg = new RolePermissionLiveSyncMessage()
                        {
                            RoleID = rolePermission.CTAMRoleID,
                            PermissionID = rolePermission.CTAMPermissionID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByRoleID.ContainsKey(rolePermission.CTAMRoleID) ? cabinetNumbersByRoleID[rolePermission.CTAMRoleID] : new List<string>()
                        };
                        collectedLiveSyncMessage.RolePermissionMessages.Add(rolePermissionMsg);
                        break;
                    case CTAMRole_Cabinet roleCabinet:
                        var roleCabinetMsg = new RoleCabinetLiveSyncMessage()
                        {
                            RoleID = roleCabinet.CTAMRoleID,
                            CabinetNumber = roleCabinet.CabinetNumber,
                            ChangeAction = changeAction
                        };
                        collectedLiveSyncMessage.RoleCabinetMessages.Add(roleCabinetMsg);
                        break;
                    case CTAMRole_ItemType roleItemType:
                        var roleItemTypeMsg = new RoleItemTypeLiveSyncMessage()
                        {
                            RoleID = roleItemType.CTAMRoleID,
                            ItemTypeID = roleItemType.ItemTypeID,
                            MaxQty = roleItemType.MaxQtyToPick,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByRoleID.ContainsKey(roleItemType.CTAMRoleID) ? cabinetNumbersByRoleID[roleItemType.CTAMRoleID] : new List<string>()
                        };
                        collectedLiveSyncMessage.RoleItemTypeMessages.Add(roleItemTypeMsg);
                        break;
                    case ItemType_ErrorCode itemTypeErrorCode:
                        var itemTypeErrorCodeMsg = new ItemTypeErrorCodeLiveSyncMessage()
                        {
                            ItemTypeID = itemTypeErrorCode.ItemTypeID,
                            ErrorCodeID = itemTypeErrorCode.ErrorCodeID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByItemTypeID.ContainsKey(itemTypeErrorCode.ItemTypeID) ? cabinetNumbersByItemTypeID[itemTypeErrorCode.ItemTypeID] : new List<string>()
                        };
                        collectedLiveSyncMessage.ItemTypeErrorCodeMessages.Add(itemTypeErrorCodeMsg);
                        break;
                    case CabinetAccessInterval cabinetAccessInterval:
                        var cabinetAccesIntervalMsg = new CabinetAccessIntervalLiveSyncMessage()
                        {
                            RoleID = cabinetAccessInterval.CTAMRoleID,
                            CabinetAccessIntervalID = cabinetAccessInterval.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByRoleID.ContainsKey(cabinetAccessInterval.CTAMRoleID) ? cabinetNumbersByRoleID[cabinetAccessInterval.CTAMRoleID] : new List<string>()
                        };
                        collectedLiveSyncMessage.CabinetAccessIntervalMessages.Add(cabinetAccesIntervalMsg);
                        break;
                    case CabinetStock cabinetStock:
                        var cabinetStockMsg = new CabinetStockLiveSyncMessage()
                        {
                            CabinetNumber = cabinetStock.CabinetNumber,
                            ItemTypeID = cabinetStock.ItemTypeID,
                            ChangeAction = changeAction
                        };
                        collectedLiveSyncMessage.CabinetStockMessages.Add(cabinetStockMsg);
                        break;
                    case CabinetPosition cabinetPosition:
                        var cabinetPositionMsg = new CabinetPositionLiveSyncMessage()
                        {
                            CabinetNumber = cabinetPosition.CabinetNumber,
                            CabinetPositionID = cabinetPosition.ID,
                            ChangeAction = changeAction
                        };
                        collectedLiveSyncMessage.CabinetPositionMessages.Add(cabinetPositionMsg);
                        break;
                    case CTAMSetting setting:
                        var settingMsg = new SettingLiveSyncMessage()
                        {
                            SettingID = setting.ID,
                            CTAMModule = setting.CTAMModule,
                            ChangeAction = changeAction
                        };
                        collectedLiveSyncMessage.SettingMessages.Add(settingMsg);
                        break;
                    // TODO: Implement in CloudUI, localui and LocalAPI, and check for user and item
                    case CTAMUserPersonalItem personalItem:

                        var cabinetNumbersByItemForPersonalItem = cabinetNumbersByItemTypeID.ContainsKey(itemToItemType[personalItem.ItemID]) ? cabinetNumbersByItemTypeID[itemToItemType[personalItem.ItemID]] : new List<string>();
                        var cabinetNumbersByUserForPersonalItem = cabinetNumbersByUserUID.ContainsKey(personalItem.CTAMUserUID) ? cabinetNumbersByUserUID[personalItem.CTAMUserUID] : new List<string>();

                        var personalItemMsg = new UserPersonalItemLiveSyncMessage()
                        {
                            UserPersonalItemID = personalItem.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByItemForPersonalItem.Intersect(cabinetNumbersByUserForPersonalItem).ToList()
                        };
                        collectedLiveSyncMessage.UserPersonalItemMessages.Add(personalItemMsg);
                        break;
                    // TODO: Implement in CloudUI, localui and LocalAPI and check for user and item depending 
                    //       on whether CTAMUserInPossession can be changed through cloudui interfaces of users and item.
                    // https://itinnovatorsbv.atlassian.net/browse/CTAM-226
                    case CTAMUserInPossession userInPossession:
                        // When an Item is picked, the other cabinets must be aware of this UserInPosession
                        // in case it gets returned to one of the other cabinets
                        // When an Item is Overdue, the other cabinets should be informed as well
                        // When an Item is Returned however the other cabinets should delete the record 
                        // as it is of no concern to the cabinets any more
                        changeAction = userInPossession.Status switch
                        {
                            UserInPossessionStatus.Picked => ChangeAction.Insert,
                            UserInPossessionStatus.Returned => ChangeAction.Delete,
                            UserInPossessionStatus.Overdue => ChangeAction.Update,
                            UserInPossessionStatus.Removed => changeAction == ChangeAction.Insert ? ChangeAction.None : (changeAction == ChangeAction.Update ? ChangeAction.Delete : ChangeAction.None),
                            _ => ChangeAction.None
                        };

                        var cabinetNumbersByItemForUserInPossession = cabinetNumbersByItemTypeID.ContainsKey(itemToItemType[userInPossession.ItemID]) ? cabinetNumbersByItemTypeID[itemToItemType[userInPossession.ItemID]] : new List<string>();
                        var uidToCheck = userInPossession.CTAMUserUIDOut != null ? userInPossession.CTAMUserUIDOut : userInPossession.CTAMUserUIDIn;
                        var cabinetNumbersByUserForUserInPossession = cabinetNumbersByUserUID.ContainsKey(uidToCheck) ? cabinetNumbersByUserUID[uidToCheck] : new List<string>();

                        var userInPossessionMsg = new UserInPossessionLiveSyncMessage()
                        {
                            UserInPossessionID = userInPossession.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = cabinetNumbersByItemForUserInPossession.Intersect(cabinetNumbersByUserForUserInPossession).ToList()
                        };
                        collectedLiveSyncMessage.UserInPossessionMessages.Add(userInPossessionMsg);
                        break;
                    case ErrorCode errorCode:
                        var errorCodeMsg = new ErrorCodeLiveSyncMessage()
                        {
                            ErrorCodeID = errorCode.ID,
                            ChangeAction = changeAction,
                            CabinetNumbers = changeAction == ChangeAction.Delete ? new List<string>()
                                                : (cabinetNumbersByErrorCodeID.ContainsKey(errorCode.ID) ? cabinetNumbersByErrorCodeID[errorCode.ID] : new List<string>())
                        };
                        collectedLiveSyncMessage.ErrorCodeMessages.Add(errorCodeMsg);
                        break;
                    default:
                        return;
                };
            }
        }
    }
}
