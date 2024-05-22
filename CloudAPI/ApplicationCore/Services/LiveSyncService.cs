using CloudAPI.ApplicationCore.Commands.Cabinet;
using CloudAPI.ApplicationCore.DTO.Sync.LiveSync;
using CloudAPI.ApplicationCore.Interfaces;
using CTAM.Core.Enums;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CloudAPI.ApplicationCore.Services
{
    public class MessageWrapper
    {
        public string Method
        {
            get;
        }

        public object Data
        {
            get;
        }

        public MessageWrapper(string method, object data)
        {
            Method = method;
            Data = data;
        }
    }

    public class LiveSyncService
    {
        private readonly IConnectedClients _connectedSignalrClients;
        private readonly ITenantContext _tenantContext;
        private readonly ILogger<LiveSyncService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private Transaction _currentTransaction;
        private readonly List<MessageWrapper> _messagesToSend;

        public LiveSyncService(IConnectedClients connectedSignalrClients, ITenantContext tenantContext, ILogger<LiveSyncService> logger, IHttpContextAccessor httpContext, IMediator mediator)
        {
            _connectedSignalrClients = connectedSignalrClients;
            _tenantContext = tenantContext;
            _logger = logger;
            _httpContextAccessor = httpContext;
            _mediator = mediator;
            _messagesToSend = new List<MessageWrapper>();
        }

        public async Task SendSingleLiveSyncMessage(MessageWrapper message)
        {
            _logger.LogInformation("Sending SignalR SingleLiveSyncMessage to the clients");

            try
            {
                if (message != null && message.Data is SingleLiveSyncMessage singleMsg)
                {
                    var sendMessageAsyncFunction = GetSendMessageAsyncFunction();
                    await sendMessageAsyncFunction(_tenantContext.TenantId, message.Method, message.Data);
                }
                else
                {
                    _logger.LogWarning($"LiveSyncService.SendSingleLiveSyncMessage: no SingleLiveSyncMessage");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LiveSyncService.SendSingleLiveSyncMessage {ex.GetMostInnerException().Message})");
            }
        }

        public async Task CollectAndSendMessage(MessageWrapper message)
        {
            _logger.LogInformation("Sending SignalR CollectAndSendMessage to the clients");

            try
            {
                if (message != null && message.Data is CollectedLiveSyncMessage collectedMsg)
                {
                    _messagesToSend.Add(message);

                    if (Transaction.Current != null)
                    {
                        // Set transcation scope completed event handler only once
                        if (_currentTransaction == null)
                        {
                            _currentTransaction = Transaction.Current;
                            _currentTransaction.TransactionCompleted += async (s, e) =>
                            {
                                if (e.Transaction.TransactionInformation.Status == TransactionStatus.Aborted)
                                {
                                    _logger.LogError("Scope is aborted. No messages will be sent.");
                                }
                                else
                                {

                                    await SendSqueezedMessageAndClearMessagesToSendListAsync();
                                }
                            };
                        }
                    }
                    else
                    {
                        await SendSqueezedMessageAndClearMessagesToSendListAsync();
                    }
                }
                else
                {
                    _logger.LogWarning($"LiveSyncService.CollectAndSendMessage: no CollectedLiveSyncMessage");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LiveSyncService.CollectAndSendMessage {ex.GetMostInnerException().Message}");
            }
        }

        public async Task SendCabinetAction(string tenant, string cabinetNumber, string action, SingleLiveSyncMessage message)
        {
            await _connectedSignalrClients.SendToCabinetInTenant(tenant, cabinetNumber, action, message);
        }

        private async Task SendSqueezedMessageAndClearMessagesToSendListAsync()
        {
            try
            {
                var sendMessageAsyncFunction = GetSendMessageAsyncFunction();
                var squeezedMessages = SqueezeMessages();

                if (squeezedMessages != null && squeezedMessages.Data is CollectedLiveSyncMessage collMessage)
                {
                    collMessage.CabinetNumbers = AssembleAllCabinetNumbers(collMessage);
                    var msgLength = JsonConvert.SerializeObject(collMessage).Length;

                    if (msgLength < 30000)
                    {
                        await sendMessageAsyncFunction(_tenantContext.TenantId, squeezedMessages.Method, squeezedMessages.Data);
                    }
                    else
                    {
                        if (collMessage.CabinetNumbers.Any())
                        {
                            _logger.LogWarning($"LiveSyncService.SendSqueezedMessageAndClearMessagesToSendListAsync: Message too big, enqueue Sync");
                            await _mediator.Send(new EnqueueForFullSyncCommand() { CabinetNumbers = collMessage.CabinetNumbers, TenantID = _tenantContext.TenantId });
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("LiveSyncService.SendSqueezedMessageAndClearMessagesToSendListAsync: squeezed empty or no CollectedLiveSyncMessage");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"LiveSyncService.SendSqueezedMessageAndClearMessagesToSendListAsync: {ex.GetMostInnerException().Message}");
            }

            _logger.LogDebug("Clearing `_messagesToSend` list");
            _messagesToSend.Clear();
        }

        /// <summary>
        /// Do a ToListAsync for all IQueryables CabinetNumbers and return a Union of all of them.
        /// </summary>
        public static List<string> AssembleAllCabinetNumbers(CollectedLiveSyncMessage collMessage)
        {
            var allCabinetNumbers = new HashSet<string>();

            collMessage.CabinetUIMessages.ForEach(x => allCabinetNumbers.UnionWith(new List<string> { x.CabinetNumber }));
            collMessage.CabinetMessages.ForEach(x => allCabinetNumbers.UnionWith(new List<string> { x.CabinetNumber }));
            collMessage.CabinetDoorMessages.ForEach(x => allCabinetNumbers.UnionWith(new List<string> { x.CabinetNumber }));
            collMessage.CabinetStockMessages.ForEach(x => allCabinetNumbers.UnionWith(new List<string> { x.CabinetNumber }));
            collMessage.CabinetPositionMessages.ForEach(x => allCabinetNumbers.UnionWith(new List<string> { x.CabinetNumber }));

            collMessage.ErrorCodeMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));

            collMessage.SettingMessages.ForEach(x => allCabinetNumbers.UnionWith(new List<string> { "DEFAULT" }));

            collMessage.ItemMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.ItemTypeMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.UserMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.RoleMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.CabinetAccessIntervalMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.UserPersonalItemMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.UserInPossessionMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.UserRoleMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.RolePermissionMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            allCabinetNumbers.UnionWith(collMessage.RoleCabinetMessages.Select(rcm => rcm.CabinetNumber));

            collMessage.RoleItemTypeMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));
            collMessage.ItemTypeErrorCodeMessages.ForEach(x => allCabinetNumbers.UnionWith(x.CabinetNumbers));

            return allCabinetNumbers.ToList();
        }

        /// <summary>
        /// Assemble all MessageWrappers in _messagesToSend messages in one.
        /// </summary>
        private MessageWrapper SqueezeMessages()
        {
            var squeezed = _messagesToSend.FirstOrDefault();

            foreach (var collMessage in _messagesToSend.Skip(1))
            {
                if (collMessage.Data is CollectedLiveSyncMessage next)
                {
                    ((CollectedLiveSyncMessage)(squeezed.Data)).CabinetUIMessages.AddRange(next.CabinetUIMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).CabinetMessages.AddRange(next.CabinetMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).CabinetStockMessages.AddRange(next.CabinetStockMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).CabinetPositionMessages.AddRange(next.CabinetPositionMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).ErrorCodeMessages.AddRange(next.ErrorCodeMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).SettingMessages.AddRange(next.SettingMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).ItemMessages.AddRange(next.ItemMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).ItemTypeMessages.AddRange(next.ItemTypeMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).UserMessages.AddRange(next.UserMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).RoleMessages.AddRange(next.RoleMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).CabinetAccessIntervalMessages.AddRange(next.CabinetAccessIntervalMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).UserPersonalItemMessages.AddRange(next.UserPersonalItemMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).UserInPossessionMessages.AddRange(next.UserInPossessionMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).UserRoleMessages.AddRange(next.UserRoleMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).RolePermissionMessages.AddRange(next.RolePermissionMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).RoleCabinetMessages.AddRange(next.RoleCabinetMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).RoleItemTypeMessages.AddRange(next.RoleItemTypeMessages);
                    ((CollectedLiveSyncMessage)(squeezed.Data)).ItemTypeErrorCodeMessages.AddRange(next.ItemTypeErrorCodeMessages);
                }
                else
                {
                    throw new Exception("SqueezeMessages should be of type CollectedLiveSyncMessage");
                }
            }

            return squeezed;
        }

        private Func<string, string, object, Task> GetSendMessageAsyncFunction()
        {
            Func<string, string, object, Task> sendMessageAsync;
            string signalRConnectionId = null;
            var keyValuePair = _httpContextAccessor.HttpContext?.Request?.Headers?.Where(header => header.Key.ToLower().Equals("x-srcid")).FirstOrDefault();
            if (!string.IsNullOrEmpty(keyValuePair?.Key))
            {
                signalRConnectionId = keyValuePair?.Value;
            }

            if (signalRConnectionId == null)
            {
                _logger.LogWarning("Cannot find SignalR ConnectionID in HTTP headers");
                if (_tenantContext.ClientType == ClientType.Cabinet)
                {
                    sendMessageAsync = (tenantID, method, data) => _connectedSignalrClients.SendToAllWebClientsInTenant(tenantID, method, data);
                }
                else
                {
                    sendMessageAsync = (tenantID, method, data) => _connectedSignalrClients.SendToAllClientsInTenant(tenantID, method, data);
                }
            }
            else
            {
                _logger.LogInformation($"SignalR ConnectionID: {signalRConnectionId}");
                sendMessageAsync = (tenantID, method, data) => _connectedSignalrClients.SendToAllClientsInTenantExcept(tenantID, method, data, new List<string> { signalRConnectionId });
            }
            return sendMessageAsync;
        }
    }
}