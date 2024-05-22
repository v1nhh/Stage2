using System;
using System.Threading.Tasks;
using CTAM.Core;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CommunicationModule.Infrastructure.Email.Services
{
    public class EmailConfigService : IEmailConfigService
    {
        private readonly ITenantService _tenantService;
        private readonly ILogger<EmailConfigService> _logger;

        public EmailConfigService(ITenantService tenantService, ILogger<EmailConfigService> logger)
        {
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<EmailSenderConfiguration> GetEmailSenderConfigurationAsync(string tenantIdentifier)
        {
            var dbConnection = _tenantService.GetConnectionStringForTenant(tenantIdentifier);
            var config = new EmailSenderConfiguration();

            using (var dbContext = _tenantService.GetDbContext(dbConnection))
            {
                // Fetch settings from the database
                config.SendGridApiKey = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.SendgridAPIKey);
                config.SmtpHost = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.SmtpHost);

                // Decision logic for which settings to use or fall back on
                if (string.IsNullOrWhiteSpace(config.SendGridApiKey))
                {
                    // No SendGrid API Key found in database, check if SMTP settings are available in database
                    if (!string.IsNullOrWhiteSpace(config.SmtpHost))
                    {
                        _logger.LogInformation($"Using database SMTP settings for tenant {tenantIdentifier}.");
                        // SMTP settings are available, use these and fetch remaining SMTP settings
                        await CompleteSmtpConfigurationFromDatabase(dbContext, config);
                    }
                    else
                    {
                        _logger.LogInformation($"Using environment variables email settings for tenant {tenantIdentifier}.");
                        // Neither SendGrid nor SMTP settings are available in the database, fall back on environment variables for both
                        FetchConfigurationFromEnvironmentVariables(config);
                    }
                }
                else
                {
                    _logger.LogInformation($"Using database SendGrid settings for tenant {tenantIdentifier}.");
                    // SendGrid API Key is available, complete the SendGrid configuration
                    await CompleteSendGridConfigurationAsync(dbContext, config);
                }
            }


            return config;

        }
        private async Task CompleteSmtpConfigurationFromDatabase(MainDbContext dbContext, EmailSenderConfiguration config)
        {
            config.SmtpPort = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.SmtpPort);
            config.SmtpUsername = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.SmtpUsername);
            config.SmtpPassword = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.SmtpPassword);
            config.MailFromAddress = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.MailFromAddress) ?? await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.SmtpFromAddress);
        }

        private async Task CompleteSendGridConfigurationAsync(MainDbContext dbContext, EmailSenderConfiguration config)
        {
            config.MailFromAddress = await FetchSettingAsync(dbContext, EmailSenderConfiguration.Keys.MailFromAddress);
        }

        private void FetchConfigurationFromEnvironmentVariables(EmailSenderConfiguration config)
        {
            config.SendGridApiKey = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SendgridAPIKey);
            config.SmtpHost = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpHost);
            config.SmtpPort = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpPort);
            config.SmtpUsername = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpUsername);
            config.SmtpPassword = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpPassword);
            config.MailFromAddress = Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.MailFromAddress) ?? Environment.GetEnvironmentVariable(EmailSenderConfiguration.Keys.SmtpFromAddress);
        }


        private async Task<string> FetchSettingAsync(MainDbContext dbContext, string settingName)
        {
            return (await dbContext.CTAMSetting().AsNoTracking().FirstOrDefaultAsync(s => s.ParName == settingName))?.ParValue;
        }
    }

}