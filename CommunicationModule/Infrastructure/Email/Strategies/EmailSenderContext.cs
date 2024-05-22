using System;
using System.Threading.Tasks;
using CommunicationModule.Infrastructure.Email.Services;
using Microsoft.Extensions.Logging;

namespace CommunicationModule.Infrastructure.Email.Strategies
{
    /// <summary>
    /// Maintains the context for the current email sender strategy and provides a mechanism to 
    /// create email sender instances based on that strategy.
    /// </summary>
    public class EmailSenderContext
    {
        private IEmailSenderStrategy _strategy; // Holds the current strategy for email sending.
        private readonly ILogger<EmailSenderContext> _logger; // Logger for capturing runtime information.

        /// <summary>
        /// Constructs the context with an injected logger.
        /// </summary>
        /// <param name="logger">The logger for recording events and errors.</param>
        public EmailSenderContext(ILogger<EmailSenderContext> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Sets the strategy for sending emails. A strategy must be set before attempting to send emails.
        /// </summary>
        /// <param name="strategy">The strategy to be used for sending emails.</param>
        public void SetStrategy(IEmailSenderStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy), "Email sender strategy cannot be null.");
            _logger.LogInformation($"Email sender strategy set to {strategy.GetType().Name}.");
        }

        /// <summary>
        /// Gets the email sender instance based on the current strategy.
        /// </summary>
        /// <param name="config">Configuration details used by the strategy to create an email sender.</param>
        /// <returns>An instance of IEmailSender based on the strategy.</returns>
        public async Task<IEmailSender> GetEmailSenderAsync(EmailSenderConfiguration config)
        {
            if (_strategy == null)
            {
                _logger.LogError("Email sender strategy has not been set.");
                throw new InvalidOperationException("Email sender strategy has not been set.");
            }

            try
            {
                return await _strategy.CreateEmailSenderAsync(config);
            }
            catch (Exception ex)
            {
                LogConfigurationAttempts(config);
                _logger.LogError(ex, "An error occurred while creating the email sender.");
                throw;
            }
        }

        /// <summary>
        /// Logs the configuration attempts using both environment variables and directly provided configuration values.
        /// This is used for troubleshooting configuration issues.
        /// </summary>
        /// <param name="config">The email configuration attempted.</param>
        private void LogConfigurationAttempts(EmailSenderConfiguration config)
        {
            // Environment Variables using EmailSenderConfiguration.Keys for key names
            var envSendgridApiKey = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SendgridAPIKey);
            var envSmtpHost = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpHost);
            var envSmtpPort = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpPort);
            var envSmtpUsername = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpUsername);
            var envSmtpFromAddress = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.MailFromAddress);

            // Log environment variables
            _logger.LogWarning("Email configuration attempted with the following environment variables:");
            _logger.LogWarning($"{EmailSenderConfiguration.Keys.SendgridAPIKey}: '{envSendgridApiKey}', {EmailSenderConfiguration.Keys.SmtpHost}: '{envSmtpHost}', {EmailSenderConfiguration.Keys.SmtpPort}: '{envSmtpPort}', {EmailSenderConfiguration.Keys.SmtpUsername}: '{envSmtpUsername}', {EmailSenderConfiguration.Keys.MailFromAddress}: '{envSmtpFromAddress}'");

            // Config Values from EmailSenderConfiguration
            _logger.LogWarning("Email configuration attempted with the following settings from EmailSenderConfiguration:");
            _logger.LogWarning($"{EmailSenderConfiguration.Keys.SendgridAPIKey}: '{config.SendGridApiKey}', {EmailSenderConfiguration.Keys.MailFromAddress}: '{config.MailFromAddress}'");
            _logger.LogWarning($"{EmailSenderConfiguration.Keys.SmtpHost}: '{config.SmtpHost}', {EmailSenderConfiguration.Keys.SmtpPort}: '{config.SmtpPort}', {EmailSenderConfiguration.Keys.SmtpUsername}: '{config.SmtpUsername}', {EmailSenderConfiguration.Keys.SmtpPassword}: '********'");
        }
    }




}