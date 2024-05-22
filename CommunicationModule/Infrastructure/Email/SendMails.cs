using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using CTAM.Core;
using CommunicationModule.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using CommunicationModule.ApplicationCore.Enums;
using System.Threading.Tasks;
using CommunicationModule.Infrastructure.Email.Strategies;
using CommunicationModule.Infrastructure.Email.Strategies.Sendgrid;
using CommunicationModule.Infrastructure.Email.Strategies.Smtp;
using CommunicationModule.Infrastructure.Email.Services;
using CTAM.Core.Interfaces;

namespace CommunicationModule.Infrastructure.Email
{
    /// <summary>
    /// Handles the processing and sending of emails for tenants in a multi-tenant application context.
    /// Utilizes tenant-specific configurations for email sending strategies.
    /// </summary>
    public class SendMails
    {
        // Dependency-injected services and configurations.
        private readonly ILogger<SendMails> _logger;
        private readonly ITenantService _tenantService;
        private readonly IEmailConfigService _emailConfigService;
        private readonly EmailSenderContext _emailSenderContext;
        private readonly ILoggerFactory _loggerFactory;

        // The default number of retries for sending an email, which can be overridden by environment variables.
        private int _emailRetryCount = 3;
        private const string SETTING_EMAILMAXRETRIES = "EmailMaxRetries";

        /// <summary>
        /// Initializes a new instance of the <see cref="SendMails"/> class with necessary services.
        /// </summary>
        /// <remarks>
        /// The email retry count is configurable via environment variables to allow easy adjustments without code changes.
        /// </remarks>
        public SendMails(ITenantService tenantService, EmailSenderContext emailSenderContext, IEmailConfigService emailConfigService, ILoggerFactory loggerFactory)
        {
            _tenantService = tenantService;
            _emailConfigService = emailConfigService;
            _emailSenderContext = emailSenderContext;
            _logger = loggerFactory.CreateLogger<SendMails>();
            _loggerFactory = loggerFactory;
            // Environment variable override for retry count allows for flexible deployment configurations.
            _emailRetryCount = int.TryParse(Environment.GetEnvironmentVariable(SETTING_EMAILMAXRETRIES), out var maxRetries) ? maxRetries : _emailRetryCount;
        }

        /// <summary>
        /// Processes and sends emails for all tenants asynchronously.
        /// </summary>
        /// <remarks>
        /// Iterates over each tenant, processing emails in batches as configured. Ensures scalability across multiple tenants.
        /// </remarks>
        public async Task SendMailsForTenantConnectionsAsync()
        {
            var tenantConnections = _tenantService.GetTenantConnections();
            foreach (var (tenantKey, tenantValue) in tenantConnections)
            {
                await ProcessEmailsForTenant(tenantKey, tenantValue);
            }
        }

        /// <summary>
        /// Processes and sends emails for a specific tenant.
        /// </summary>
        /// <param name="tenantKey">The key identifying the tenant.</param>
        /// <param name="tenantValue">The value or identifier used to access tenant-specific resources.</param>
        /// <remarks>
        /// Manages database context per tenant to ensure data isolation. Handles email sending and error logging for audit trails.
        /// </remarks>
        private async Task ProcessEmailsForTenant(string tenantKey, string tenantValue)
        {
            using var dbContext = _tenantService.GetDbContext(tenantValue);
            var mailQueueItems = FetchMailsToSendForTenant(dbContext);

            if (!mailQueueItems.Any())
            {
                _logger.LogInformation($"No emails to send for tenant {tenantKey}.");
                return;
            }

            var emailSender = await GetEmailSenderForTenant(tenantKey);
            if (emailSender == null) return;

            foreach (var mailQueueItem in mailQueueItems)
            {
                await SendEmailAndUpdateStatus(mailQueueItem, emailSender, dbContext);
            }

            await dbContext.SaveChangesAsync();
            await emailSender.DisconnectAsync();
        }

        /// <summary>
        /// Attempts to send an email and update its status in the database.
        /// </summary>
        /// <param name="mailQueueItem">The mail queue item representing the email to be sent.</param>
        /// <param name="emailSender">The email sender strategy for the current tenant.</param>
        /// <param name="dbContext">The database context for the tenant.</param>
        /// <remarks>
        /// If sending fails, it retries up to a configured maximum. On success or final failure, updates the email's status.
        /// </remarks>
        private async Task SendEmailAndUpdateStatus(MailQueue mailQueueItem, IEmailSender emailSender, MainDbContext dbContext)
        {
            bool sentSuccessfully = await TrySendEmailAsync(mailQueueItem, emailSender);

            if (sentSuccessfully)
            {
                // Update email status to Sent on successful delivery.
                mailQueueItem.Status = MailQueueStatus.Sent;
                mailQueueItem.LastFailedErrorMessage = null; // Clear any previous error messages on success.
                _logger.LogInformation($"Email to {mailQueueItem.MailTo} sent successfully.");
            }
            else
            {
                // Increment failed attempts and possibly mark as failed.
                mailQueueItem.FailedAttempts++;
                if (mailQueueItem.FailedAttempts >= _emailRetryCount)
                {
                    mailQueueItem.Status = MailQueueStatus.Failed; // Mark as failed after exceeding retry limit.
                    _logger.LogWarning($"Email to {mailQueueItem.MailTo} marked as failed after {_emailRetryCount} attempts.");
                }
            }
        }

        /// <summary>
        /// Retrieves a configured email sender based on the tenant's settings.
        /// </summary>
        /// <param name="tenantKey">The key identifying the tenant.</param>
        /// <returns>An instance of <see cref="IEmailSender"/> configured for the tenant, or null if configuration fails.</returns>
        /// <remarks>
        /// Selects the appropriate email sending strategy (e.g., SendGrid, SMTP) based on the tenant's configuration.
        /// </remarks>
        private async Task<IEmailSender> GetEmailSenderForTenant(string tenantKey)
        {
            try
            {
                var config = await _emailConfigService.GetEmailSenderConfigurationAsync(tenantKey);

                if (!string.IsNullOrWhiteSpace(config.SendGridApiKey))
                {
                    // Use SendGrid strategy if API key is present.
                    _emailSenderContext.SetStrategy(new SendGridEmailSenderStrategy(_loggerFactory));
                }
                else if (!string.IsNullOrWhiteSpace(config.SmtpHost))
                {
                    // Use SMTP strategy if host is defined.
                    _emailSenderContext.SetStrategy(new SmtpEmailSenderStrategy(_loggerFactory));
                }
                else
                {
                    // Log an error and return null if no valid configuration is found.
                    _logger.LogError($"No valid email configuration found for tenant {tenantKey}.");
                    return null;
                }

                return await _emailSenderContext.GetEmailSenderAsync(config);
            }
            catch (Exception ex)
            {
                // Log exceptions during the email sender retrieval process.
                _logger.LogError(ex, $"Failed to create email sender for tenant {tenantKey}. Skipping to the next tenant.");
                return null;
            }
        }

        /// <summary>
        /// Fetches emails that are ready to be sent for a tenant, applying any configured bulk send limits.
        /// </summary>
        /// <param name="dbContext">The database context for the tenant.</param>
        /// <returns>An IQueryable of <see cref="MailQueue"/> representing the emails to send.</returns>
        private static IQueryable<MailQueue> FetchMailsToSendForTenant(MainDbContext dbContext)
        {
            // Retrieve bulk mail send limit, if configured.
            var bulkAmount = dbContext.CTAMSetting()
                                    .AsNoTracking()
                                    .Where(s => s.ParName.Equals(EmailSenderConfiguration.Keys.bulk_mail_amount))
                                    .Select(s => s.ParValue)
                                    .FirstOrDefault();

            // Fetch emails marked as Created and ready for sending, applying the bulk send limit if it exists.
            var mailQueueItems = dbContext.MailQueue()
                .Include(mq => mq.MailMarkupTemplate) // Include necessary related data.
                .OrderByDescending(m => m.Prio) // Prioritize emails by priority.
                .Where(m => m.Status == MailQueueStatus.Created); // Only include emails that haven't been sent or failed.

            if (bulkAmount != null && int.TryParse(bulkAmount.ToString(), out var amount))
            {
                // Apply bulk send limit to the query if it's valid.
                mailQueueItems = mailQueueItems.Take(amount);
            }

            return mailQueueItems;
        }

        /// <summary>
        /// Attempts to send an email using the provided email sender.
        /// </summary>
        /// <param name="mailQueueItem">The mail queue item to be sent.</param>
        /// <param name="emailSender">The email sender.</param>
        /// <returns>True if the email was sent successfully; otherwise, false.</returns>
        /// <remarks>
        /// Logs the attempt and any failures. Increments the failed attempt counter on failure.
        /// </remarks>
        private async Task<bool> TrySendEmailAsync(MailQueue mailQueueItem, IEmailSender emailSender)
        {
            try
            {
                await emailSender.SendEmailAsync(mailQueueItem); // Attempt to send the email.
                return true; // Return true on success.
            }
            catch (Exception ex)
            {
                // Log the exception and return false on failure.
                _logger.LogError(ex, $"Failed to send email to {mailQueueItem.MailTo}. Attempt {mailQueueItem.FailedAttempts + 1} of {_emailRetryCount} with the following exception {ex.Message}");
                mailQueueItem.LastFailedErrorMessage = ex.Message; // Store the failure message for later analysis.
                return false;
            }
        }
    }
}



