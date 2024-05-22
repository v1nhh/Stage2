using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudAPI.ApplicationCore.Interfaces
{
    public interface IConnectedClients
    {
        /// <summary>
        /// Add connectionId of client to SignalR Group of tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task AddClient(string tenantID, string connectionId);

        /// <summary>
        /// Add connectionId of client to SignalR Group of tenant and to tenant based web clients SignalR Group
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task AddWebClient(string tenantID, string connectionId);

        /// <summary>
        /// Add connectionId of client to SignalR Group of tenant, to tenant based cabinets SignalR Group and to single-cabinet SignalR Group
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="connectionId"></param>
        /// <param name="cabinetNumber"></param>
        /// <returns></returns>
        public Task AddCabinet(string tenantID, string connectionId, string cabinetNumber);

        /// <summary>
        /// Remove connectionId of client from SignalR Group of tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task RemoveClient(string tenantID, string connectionId);

        /// <summary>
        /// Remove connectionId of client from SignalR Group of tenant and from tenant based web clients SignalR Group
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public Task RemoveWebClient(string tenantID, string connectionId);

        /// <summary>
        /// Remove connectionId of client from SignalR Group of tenant, from tenant based cabinets SignalR Group and from single-cabinet SignalR Group
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="connectionId"></param>
        /// <param name="cabinetNumber"></param>
        /// <returns></returns>
        public Task RemoveCabinet(string tenantID, string connectionId, string cabinetNumber);

        /// <summary>
        /// Send to <b>ALL</b> clients <b>across all tenants</b>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendToAllClients(string method, object data);

        /// <summary>
        /// Send to all clients except for provided connectionIDs
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToAllClientsExcept(string method, object data, IReadOnlyList<string> excludedConnectionIds);

        /// <summary>
        /// Send to all clients within one tenant based on provided tenantID
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToAllClientsInTenant(string tenantID, string method, object data);

        /// <summary>
        /// Send to all clients within one tenant based on provided tenantID except for provided connectionIDs
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToAllClientsInTenantExcept(string tenantID, string method, object data, IReadOnlyList<string> excludedConnectionIds);

        /// <summary>
        /// Send to all web clients within one tenant based on provided tenantID
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToAllWebClientsInTenant(string tenantID, string method, object data);

        /// <summary>
        /// Send to all cabinets within one tenant based on provided tenantID
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToAllCabinetsInTenant(string tenantID, string method, object data);

        /// <summary>
        /// Send to a specific cabinet within one tenant based on provided tenantID and cabinetNumber
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToCabinetInTenant(string tenantID, string cabinetNumber, string method, object data);

        /// <summary>
        /// Send to a specific client based on provided connectionId
        /// </summary>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task SendToClient(string connectionId, string method, object data);

        public IEnumerable<string> GetAllConnectedCabinetNumbers();

        public IEnumerable<string> GetAllConnectedTenants();

        public IEnumerable<string> GetConnectedCabinetNumbersForTenant(string tenantID);
    }
}