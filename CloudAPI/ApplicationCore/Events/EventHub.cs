using CabinetModule.ApplicationCore.Commands.Cabinets;
using CabinetModule.ApplicationCore.DTO.SignalR;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CloudAPI.ApplicationCore.Commands.Cabinet;
using CloudAPI.ApplicationCore.Interfaces;
using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudAPI.Web.Events
{
    //[Authorize(Roles = "MANAGEMENT_READ")] // For testing
    [Authorize(Roles = "CABINET_ACTIONS,MANAGEMENT_READ")]
    public class EventHub : Hub
    {
        private readonly ILogger<EventHub> _logger;
        private readonly IConnectedClients _connectedClients;
        private readonly IServiceProvider _serviceProvider;

        public EventHub(ILogger<EventHub> logger, IConnectedClients connectedClients, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _connectedClients = connectedClients;
            _serviceProvider = serviceProvider;
        }

        public async Task SendToClient(string connectionId, Connected payload)
        {
            try
            {
                var payloadSerialized = JsonConvert.SerializeObject(payload);
                await _connectedClients.SendToClient(connectionId, "ReceiveMessage", payloadSerialized);
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not send message of type");
                _logger.LogError(e.Message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var tenantID = GetClaim(CTAMClaimNames.TenandID);
            var clientTypeName = GetClaim(CTAMClaimNames.ClientType);
            var clientType = ParseClientTypeFromName(clientTypeName);
            if (tenantID == null)
            {
                _logger.LogError($"Failed to authorize for a SignalR connection: ClientType: {clientTypeName}, UserIdentifier: {Context.UserIdentifier}");
                Context.Abort();
                return;
            }
            _logger.LogInformation($"Client CONNECTED\n Type: {clientTypeName}\n TenantID: '{tenantID}'\n Identifier: '{Context.UserIdentifier}'");
            var obj = new Connected
            {
                ConnectionId = Context.ConnectionId,
                ServerId = Environment.GetEnvironmentVariable("ID")
            };
            await AddClient(clientType, tenantID);
            await SendToClient(Context.ConnectionId, obj); // LocalAPI ReceiveMessage 
            await base.OnConnectedAsync();
        }

        private async Task AddClient(ClientType clientType, string tenantID)
        {
            if (clientType == ClientType.Cabinet)
            {
                string cabinetNumber = Context.UserIdentifier;
                _logger.LogInformation($"TenantID={tenantID} - Adding cabinet with cabinetNumber: {cabinetNumber}");
                try
                {
                    await _connectedClients.AddCabinet(tenantID, Context.ConnectionId, cabinetNumber);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to add Cabinet {cabinetNumber} to the cabinet group of Tenant {tenantID}");
                    _logger.LogError(e.Message);
                    return;
                }
                // Update state of the cabinet and notify web clients
                // Onderstaande triggert een synccollected waardoor LocalAPI dit opvangt en zijn eigen status update via de MakeCollectedChangesCommand
                // Deze vraagt de CloudAPI weer voor de hele Cabinet row en slaat deze op in de localapi. Dit is in principe dan al in de CloudDB aanwezig en op
                // status syncing van deze cabinet. Dus hij gaat hierdoor na ontvangen van dit bericht toch opnieuw de CloudAPI vragen om de hele cabinet row.
                await ExecuteWithinTenantScope(async (IMediator m) =>
                {
                    var cabinet = await m.Send(new GetCabinetByCabinetNumberQuery(cabinetNumber));
                    // only used for cabinets that are already used
                    // initial syncing is already handled by cabinet itself
                    if(cabinet.Status == CabinetStatus.Initial)
                    {
                        return;
                    }
                    var result = await m.Send(new EnqueueForFullSyncCommand() { CabinetNumbers = new List<string>() { cabinetNumber }, TenantID = tenantID });
                    _logger.LogInformation($"Enqueued CabinetNumber: {cabinetNumber})");
                });
                //await UpdateCabinetStatus(cabinetNumber, CabinetStatus.Syncing);
                // IBKs = n, n times connected to maindbcontext
                //De methode hieronder wordt naar CloudUI gecommuniceerd, maar die doet er niks mee
                //await Clients.Group(tenantID).SendAsync("CabinetStatus", new CabinetStatusUpdateDTO(cabinetNumber, CabinetStatus.Syncing));
                await _connectedClients.SendToAllWebClientsInTenant(tenantID, "ReceiveMessage", $"Cabinet with cabinetNumber {cabinetNumber} is joined"); // CloudUI ReceiveMessage doet er niks mee, maar misschien cabinet wel
            }
            else if (clientType == ClientType.Web)
            {
                try
                {
                    await _connectedClients.AddWebClient(tenantID, Context.ConnectionId);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Failed to add Web client {Context.UserIdentifier} to the web group of Tenant {tenantID}");
                    _logger.LogError(e.Message);
                    return;
                }
            }
            else
            {
                _logger.LogInformation($"Client with unknown identifier connection: ClientType: {clientType.GetDisplayName()}");
                await _connectedClients.AddClient(tenantID, Context.ConnectionId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (exception != null)
            {
                _logger.LogError(exception, "SIGNALR");
            }
            var tenantID = GetClaim(CTAMClaimNames.TenandID);
            var clientTypeName = GetClaim(CTAMClaimNames.ClientType);
            var clientType = ParseClientTypeFromName(clientTypeName);
            string connectionId = Context.ConnectionId;
            _logger.LogInformation($"Client DISCONNECTED\n Type: {clientTypeName}\n TenantID: '{tenantID}'\n Identifier: '{Context.UserIdentifier}'");

            if (clientType == ClientType.Cabinet)
            {
                var cabinetNumber = Context.UserIdentifier;
                await _connectedClients.RemoveCabinet(tenantID, connectionId, cabinetNumber);
                await ExecuteWithinTenantScope(async (IMediator m) =>
                {
                    var cabinet = await m.Send(new UpdateCabinetStatusCommand(cabinetNumber, CabinetStatus.Offline));
                    _logger.LogInformation($"Disconnect CabinetNumber: {cabinetNumber} (status: {Enum.GetName(typeof(CabinetStatus), cabinet.Status)})");
                });

                await _connectedClients.SendToAllWebClientsInTenant(tenantID, "CabinetStatus", new CabinetStatusUpdateDTO(cabinetNumber, CabinetStatus.Offline));
                await _connectedClients.SendToAllWebClientsInTenant(tenantID, "ReceiveMessage", $"Cabinet with cabinetNumber {cabinetNumber} has left");
            }
            else if (clientType == ClientType.Web)
            {
                await _connectedClients.RemoveWebClient(tenantID, Context.ConnectionId);
            }
            else if (clientType == ClientType.Unknown)
            {
                await _connectedClients.RemoveClient(tenantID, connectionId);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public string ClientString()
        {
            return $"[TenantID={GetClaim(CTAMClaimNames.TenandID)}|{GetClaim(CTAMClaimNames.ClientType)}|ClientID={Context.UserIdentifier}]";
        }

        private string GetClaim(string claimName)
        {
            return Context.GetHttpContext().User.Claims?.Where(c => c.Type == claimName)?.FirstOrDefault()?.Value;
        }

        private ClientType ParseClientTypeFromName(string clientTypeName)
        {
            if (clientTypeName == null)
            {
                return ClientType.Unknown;
            }
            return (ClientType)Enum.Parse(typeof(ClientType), clientTypeName);
        }

        /**
         * Execute code by creating a scope and providing a TenantContext object with information about the client(TenantId, ClientId)
         * Use this method to call handlers when processing incoming SignalR messages
         * Example:
         *      await ExecuteWithinTenantScope(async (m) =>
         *      {
         *          await m.Send(new UpdateCabinetStatusCommand(cabinetNumber, status));
         *      });
         */
        private async Task ExecuteWithinTenantScope(Func<IMediator, Task> executor)
        {
            var tenantId = GetClaim(CTAMClaimNames.TenandID);
            var clientType = ParseClientTypeFromName(GetClaim(CTAMClaimNames.ClientType));
            var clientId = Context.UserIdentifier;
            using (var scope = _serviceProvider.CreateScope())
            {
                var tenantContext = scope.ServiceProvider.GetService<ITenantContext>();
                if (clientType == ClientType.Cabinet)
                {
                    tenantContext.SetCabinetContext(tenantId, clientId);
                }
                else if (clientType == ClientType.Web)
                {
                    tenantContext.SetWebUserContext(tenantId, clientId);
                }
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await executor(mediator);
            }
        }

    }

    public class Connected
    {
        public string ConnectionId { get; set; }
        public string ServerId { get; set; }
    }


    // For testing
    public class Message
    {
        public string type { get; set; }
        public int[] list { get; set; }
        public override string ToString()
        {
            return $"Message {type}";
        }
    }
}