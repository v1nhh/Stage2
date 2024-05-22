
using CloudAPI.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudAPI.Web.Events
{
    /// <summary>
    /// There are in total 4 different kinds of SignalR groups: <br/>
    ///   - One group for each tenant, based on tenantID <br/>
    ///   - One group for each cabinet, based on combination of CabinetNumber and tenantID <br/>
    ///   - One group for all cabinets within a tenant, based on tenantID and a constant <br/>
    ///   - One group for all web clients within a tenant, based on tenantID and a constant
    /// </summary>
    public class ConnectedClients: IConnectedClients
    {
        private readonly ILogger<ConnectedClients> _logger;
        private readonly IHubContext<EventHub> _eventHub;
        private const string CABINETS_GROUP = "CABINETS_GROUP";
        private const string WEB_GROUP = "WEB_GROUP";
        private readonly Dictionary<string, HashSet<string>> _cabinetConnectionsPerTenant = new Dictionary<string, HashSet<string>>();

        public ConnectedClients(ILogger<ConnectedClients> logger, IHubContext<EventHub> eventHub)
        {
            _logger = logger;
            _eventHub = eventHub;
        }

        public async Task AddClient(string tenantID, string connectionId)
        {
            try
            {
                await _eventHub.Groups.AddToGroupAsync(connectionId, tenantID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task AddWebClient(string tenantID, string connectionId)
        {
            try
            {
                await AddClient(tenantID, connectionId);
                await _eventHub.Groups.AddToGroupAsync(connectionId, GetTenantSubgroupKey(tenantID, WEB_GROUP));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task AddCabinet(string tenantID, string connectionId, string cabinetNumber)
        {
            try
            {
                await AddClient(tenantID, connectionId);

                await _eventHub.Groups.AddToGroupAsync(connectionId, GetTenantSubgroupKey(tenantID, CABINETS_GROUP));

                if (!_cabinetConnectionsPerTenant.ContainsKey(tenantID))
                {
                    _cabinetConnectionsPerTenant[tenantID] = new HashSet<string>();
                }
                _cabinetConnectionsPerTenant[tenantID].Add(cabinetNumber);
                await _eventHub.Groups.AddToGroupAsync(connectionId, GetTenantSubgroupKey(tenantID, cabinetNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task RemoveClient(string tenantID, string connectionId)
        {
            try
            {
                await _eventHub.Groups.RemoveFromGroupAsync(connectionId, tenantID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task RemoveWebClient(string tenantID, string connectionId)
        {
            try
            {
                await RemoveClient(tenantID, connectionId);
                await _eventHub.Groups.RemoveFromGroupAsync(connectionId, GetTenantSubgroupKey(tenantID, WEB_GROUP));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task RemoveCabinet(string tenantID, string connectionId, string cabinetNumber)
        {
            try
            { 
                await RemoveClient(tenantID, connectionId);

                await _eventHub.Groups.RemoveFromGroupAsync(connectionId, GetTenantSubgroupKey(tenantID, CABINETS_GROUP));

                if (_cabinetConnectionsPerTenant.ContainsKey(tenantID))
                {
                    _cabinetConnectionsPerTenant[tenantID].Remove(cabinetNumber);
                    if (_cabinetConnectionsPerTenant[tenantID].Count == 0)
                    {
                        _cabinetConnectionsPerTenant.Remove(tenantID);
                    }
                }
                await _eventHub.Groups.RemoveFromGroupAsync(connectionId, GetTenantSubgroupKey(tenantID, cabinetNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        // not used?
        public async Task SendToAllClients(string method, object data)
        {
            try
            { 
                await _eventHub.Clients.All.SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        // not used?
        public async Task SendToAllClientsExcept(string method, object data, IReadOnlyList<string> excludedConnectionIds)
        {
            try
            {
                await _eventHub.Clients.AllExcept(excludedConnectionIds).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task SendToAllClientsInTenant(string tenantID, string method, object data)
        {
            try
            {
                await _eventHub.Clients.Group(tenantID).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task SendToAllClientsInTenantExcept(string tenantID, string method, object data, IReadOnlyList<string> excludedConnectionIds)
        {
            try
            { 
                await _eventHub.Clients.GroupExcept(tenantID, excludedConnectionIds).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task SendToAllWebClientsInTenant(string tenantID, string method, object data)
        {
            try
            {
                await _eventHub.Clients.Group(GetTenantSubgroupKey(tenantID, WEB_GROUP)).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        // not used?
        public async Task SendToAllCabinetsInTenant(string tenantID, string method, object data)
        {
            try
            {
                await _eventHub.Clients.Group(GetTenantSubgroupKey(tenantID, CABINETS_GROUP)).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        // not used?
        public async Task SendToCabinetInTenant(string tenantID, string cabinetNumber, string method, object data)
        {
            try
            {
                await _eventHub.Clients.Group(GetTenantSubgroupKey(tenantID, cabinetNumber)).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        public async Task SendToClient(string connectionId, string method, object data)
        {
            try
            { 
                await _eventHub.Clients.Client(connectionId).SendAsync(method, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
        }

        private string GetTenantSubgroupKey(string tenantID, string subgroupID)
        {
            return $"{tenantID}_{subgroupID}";
        }

        public IEnumerable<string> GetAllConnectedCabinetNumbers()
        {
            return _cabinetConnectionsPerTenant.SelectMany(kv => kv.Value).ToList();
        }

        public IEnumerable<string> GetAllConnectedTenants()
        {
            var connectedTenants = _cabinetConnectionsPerTenant.Select(kv => kv.Key).ToList();
            return connectedTenants;
        }

        public IEnumerable<string> GetConnectedCabinetNumbersForTenant(string tenantID)
        {
            if (_cabinetConnectionsPerTenant.TryGetValue(tenantID, out var cabinetNumbers))
            {
                return cabinetNumbers.ToList();
            }
            return Enumerable.Empty<string>();
        }
    }
}