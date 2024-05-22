using System.Collections.Generic;

namespace CTAM.Core.Interfaces
{
    public interface ITenantService
    {
        Dictionary<string, string> GetTenantConnections();
        string GetLicenseKeyForTenant(string tenant);
        string GetConnectionStringForTenant(string tenant);
        LicenseFields GetLicenseFieldsForTenant(string tenant);
        List<string> GetLicensePermissionsForTenant(string tenant);
        MainDbContext GetDbContext(string connection);
    }
}
