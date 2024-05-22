using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CommunicationModule.ApplicationCore.Entities;
using System;

namespace CommunicationModule.Infrastructure.Email.Strategies.Sendgrid
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly string _sendGridFromAddress;
        private readonly ILogger<SendGridEmailSender> _logger;

        public SendGridEmailSender(string apiKey, string sendGridFromAddress, ILogger<SendGridEmailSender> logger)
        {
            _client = new SendGridClient(apiKey);
            _sendGridFromAddress = sendGridFromAddress;
            _logger = logger;
        }

        public async Task SendEmailAsync(MailQueue mailQueue)
        {
            var from = new EmailAddress(_sendGridFromAddress, "Connect Asset Manager");
            var to = new EmailAddress(mailQueue.MailTo);
            var htmlBody = mailQueue.MailMarkupTemplate.Template.Replace(@"{{body}}", mailQueue.Body);
            var msg = MailHelper.CreateSingleEmail(from, to, mailQueue.Subject, "", htmlBody);


            var response = await _client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new InvalidOperationException($"Failed to send email to {mailQueue.MailTo}. Status code: {response.StatusCode} {response.Body.ReadAsStringAsync().Result}");
            }
        }

        public Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }
    }
}
