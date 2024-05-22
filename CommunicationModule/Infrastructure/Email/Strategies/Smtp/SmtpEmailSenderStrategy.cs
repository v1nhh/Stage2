using System;
using System.Threading.Tasks;
using CommunicationModule.Infrastructure.Email.Services;
using Microsoft.Extensions.Logging;

namespace CommunicationModule.Infrastructure.Email.Strategies.Smtp
{
    public class SmtpEmailSenderStrategy : IEmailSenderStrategy
    {
        private readonly ILogger<SmtpEmailSenderStrategy> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public SmtpEmailSenderStrategy(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<SmtpEmailSenderStrategy>();
        }

        public Task<IEmailSender> CreateEmailSenderAsync(EmailSenderConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.SmtpHost))
            {
                _logger.LogError("SMTP host is missing. Cannot create SmtpEmailSender.");
                throw new InvalidOperationException("SMTP host is not provided.");
            }

            _logger.LogInformation($"Creating SmtpEmailSender with host: {config.SmtpHost}.");
            var smtpPort = int.TryParse(config.SmtpPort, out int port) ? port : throw new InvalidOperationException("Invalid SMTP port.");
            var logger = _loggerFactory.CreateLogger<SmtpEmailSender>();
            var emailSender = new SmtpEmailSender(config.SmtpHost, smtpPort, config.SmtpUsername, config.SmtpPassword, config.MailFromAddress, logger);
            return Task.FromResult<IEmailSender>(emailSender);
        }
    }


}