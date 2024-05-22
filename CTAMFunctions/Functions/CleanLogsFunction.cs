using CloudAPI.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Enums;

namespace CTAMFunctions
{
    public class CleanLogsFunction
    {
        private const string EVERY_30_SECONDS = "*/30 * * * * *";
        private const string EVERY_DAY = "0 0 0 * * *";

        private CleanLogs _cleanLogs;
        private ILogger _logger;

        public CleanLogsFunction(CleanLogs cleanLogs)
        {
            _cleanLogs = cleanLogs;
        }

        [FunctionName("CleanLogsFunction")]
        [Singleton]
        public async Task Run([TimerTrigger(EVERY_DAY, RunOnStartup = true)] TimerInfo myTimer, ILogger logger)
        {
            _logger = logger;
            _logger.LogInformation($"CleanLogsFunction: C# Timer trigger function executed at: {DateTime.UtcNow.ToString("o")}");
            try
            {
                await _cleanLogs.CleanLogsForTenantConnections(LogType.Operational);
                await _cleanLogs.CleanLogsForTenantConnections(LogType.Technical);
                await _cleanLogs.CleanLogsForTenantConnections(LogType.Management);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "CleanLogsFunction failed");
            }
        }
    }
}