using CommunicationModule;
using CommunicationModule.Infrastructure.Email;

namespace CTAMCron
{
    public class SendMailsFunction : BackgroundService
    {

        private readonly ILogger<SendMailsFunction> _logger;
        private SendMails _sendMails;

        public SendMailsFunction(ILogger<SendMailsFunction> logger, SendMails sendMails)
        {
            _logger = logger;
            _sendMails = sendMails;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"SendMailsFunction: C# Timer trigger function executed at: {DateTime.UtcNow.ToString("o")}");
            while (!stoppingToken.IsCancellationRequested)
            {
                await _sendMails.SendMailsForTenantConnectionsAsync();
                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}