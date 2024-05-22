using CTAM.Core.Enums;

namespace CTAM.Core.Interfaces
{
    public interface ITenantContext
    {
        string TenantId { get; }
        ClientType ClientType { get; }
        string ClientId { get; }
        void SetWebUserContext(string tenantId, string userUID);
        void SetCabinetContext(string tenantId, string cabinetNumber);
        void SetTenant(string tenantId);
    }
}
