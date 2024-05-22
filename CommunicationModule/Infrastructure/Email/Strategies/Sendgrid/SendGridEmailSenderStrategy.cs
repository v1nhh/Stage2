using System;
using System.Threading.Tasks;
using CommunicationModule.Infrastructure.Email.Services;
using Microsoft.Extensions.Logging;

namespace CommunicationModule.Infrastructure.Email.Strategies.Sendgrid
{
    public class SendGridEmailSenderStrategy : IEmailSenderStrategy
    {
        private readonly ILogger<SendGridEmailSenderStrategy> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public SendGridEmailSenderStrategy(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<SendGridEmailSenderStrategy>();
        }

        public Task<IEmailSender> CreateEmailSenderAsync(EmailSenderConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.SendGridApiKey))
            {
                _logger.LogError("SendGrid API key is missing. Cannot create SendGridEmailSender.");
                throw new InvalidOperationException("SendGrid API key is not provided.");
            }

            _logger.LogInformation("Creating SendGridEmailSender with provided API key.");
            var logger = _loggerFactory.CreateLogger<SendGridEmailSender>();
            var emailSender = new SendGridEmailSender(config.SendGridApiKey, config.MailFromAddress, logger);
            return Task.FromResult<IEmailSender>(emailSender);
        }
    }


}