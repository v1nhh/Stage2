using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace CTAM.Core
{
    /**
     * This class contains information about the caller
     */
    public class TenantContext: ITenantContext
    {
        private readonly ILogger<TenantContext> _logger;
        private readonly HttpContext _httpContext;

        public string TenantId { get; private set; }
        public ClientType ClientType { get; private set; }
        public string ClientId { get; private set; } // Id of the Web user or Cabinet

        public TenantContext(IHttpContextAccessor httpContextAccessor, ILogger<TenantContext> logger)
        {
            _logger = logger;
            _httpContext = httpContextAccessor.HttpContext;
            if (_httpContext != null)
            {
                InitialiazeContextFromHttpContext();
            }
        }

        private void InitialiazeContextFromHttpContext()
        {
            var tenantId = GetClaim(CTAMClaimNames.TenandID) ?? _httpContext.Request.Headers["X-Tenant-ID"];
            var clientTypeName = GetClaim(CTAMClaimNames.ClientType) ?? (_httpContext.Request.Path.Value.ToLower().Contains("login/cabinet") ? "Cabinet" : "Web");
            var clientId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(clientTypeName) || !Enum.IsDefined(typeof(ClientType), clientTypeName))
            {
                return;
            }
            var clientType = (ClientType)Enum.Parse(typeof(ClientType), clientTypeName);
            switch (clientType)
            {
                case ClientType.Cabinet:
                    SetCabinetContext(tenantId, clientId);
                    break;
                case ClientType.Web:
                    SetWebUserContext(tenantId, clientId);
                    break;
            }
        }

        private string GetClaim(string name)
        {
            return _httpContext.User?.Claims?
                .FirstOrDefault(claim => claim.Type.Equals(name))?.Value;
        }

        public void SetWebUserContext(string tenantId, string userUID)
        {
            TenantId = tenantId;
            ClientId = userUID;
            ClientType = ClientType.Web;
        }

        public void SetCabinetContext(string tenantId, string cabinetNumber)
        {
            TenantId = tenantId;
            ClientId = cabinetNumber;
            ClientType = ClientType.Cabinet;
        }

        public void SetTenant(string tenantId)
        {
            TenantId = tenantId;
        }

        override public string ToString()
        {
            return $"Tenant={TenantId}, ClientType={Enum.GetName(typeof(ClientType), ClientType)}, ClientId={ClientId}";
        }
    }
}