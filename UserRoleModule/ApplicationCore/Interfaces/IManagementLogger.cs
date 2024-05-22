using System;
using System.Threading.Tasks;

namespace UserRoleModule.ApplicationCore.Interfaces
{
    public interface IManagementLogger
    {
        Task LogFatal(string logResourcePath, params (string key, string value)[] parameters);

        Task LogError(string logResourcePath, params (string key, string value)[] parameters);

        Task LogError(Exception exception);

        Task LogWarning(string logResourcePath, params (string key, string value)[] parameters);

        Task LogInfo(string logResourcePath, params (string key, string value)[] parameters);

        Task LogDebug(string logResourcePath, params (string key, string value)[] parameters);
    }
}
