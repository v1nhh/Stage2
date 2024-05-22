using CloudAPI.Services;
using UserRoleModule.ApplicationCore.Enums;

namespace CTAMCron
{
    public class CleanLogsFunction : BackgroundService
    {
        private readonly ILogger<CleanLogsFunction> _logger;
        private const string EVERY_30_SECONDS = "*/30 * * * * *";
        private const string EVERY_DAY = "0 0 0 * * *";

        private CleanLogs _cleanLogs;

        public CleanLogsFunction(ILogger<CleanLogsFunction> logger, CleanLogs cleanLogs)
        {
            _logger = logger;
            _cleanLogs = cleanLogs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
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
                await Task.Delay(86400000, stoppingToken);
            }
        }
    }
}