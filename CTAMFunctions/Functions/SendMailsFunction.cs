using CommunicationModule;
using CommunicationModule.Infrastructure.Email;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CTAMFunctions
{
    public class SendMailsFunction
    {
        private ILogger _logger;
        private SendMails _sendMails;

        public SendMailsFunction(SendMails sendMails)
        {
            _sendMails = sendMails;
        }

        //"*/1 * * * * *"
        [FunctionName("SendMailsFunction")]
        [Singleton]
        public async Task RunAsync([TimerTrigger("*/30 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            _logger = log;
            _logger.LogInformation($"SendMailsFunction: C# Timer trigger function executed at: {DateTime.UtcNow.ToString("o")}");
            await _sendMails.SendMailsForTenantConnectionsAsync();
        }
    }
}
