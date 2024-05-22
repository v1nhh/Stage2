using System.Linq;
using System.Threading.Tasks;
using CTAM.Core.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Targets;

namespace UserRoleModule
{
    // Based  on: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-3.1#per-request-middleware-dependencies
    public class NLogMiddleware
    {
        private readonly RequestDelegate _next;

        public NLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IConfiguration configuration, RsaSecurityKey rsa)
        {
            // Claims are not yet available at this point
            var authUtils = new AuthenticationUtilities(configuration);
            string tenantID = null;
            try
            {
                if (httpContext.Request.Cookies.Where(cookie => cookie.Key.StartsWith("access")).FirstOrDefault().Value != null) {
                    tenantID = authUtils.ExtractTenantIDFromToken(httpContext.Request.Cookies.Where(cookie => cookie.Key.StartsWith("access")).FirstOrDefault().Value, rsa);
                }
            }
            catch
            {
                // Don't do anything, will save logs without tenant ID. This is will be the case during login/logout flow
            }
            var filename = tenantID == null ? "${shortdate}_CtamCloudLogfile.txt" : tenantID + "_${shortdate}_CtamCloudLogfile.txt";

            var target = (FileTarget)LogManager.Configuration.FindTargetByName("logfile");
            target.FileName = $"./logs/{filename}";
            LogManager.ReconfigExistingLoggers();

            await _next(httpContext);
        }
    }
}
