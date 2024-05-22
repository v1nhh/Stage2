using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CommunicationModule.ApplicationCore.Entities;
using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace CommunicationModule.Infrastructure.Email.Strategies.Smtp
{


    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger<SmtpEmailSender> _logger;

        private string _smtpFromAddress;
        private string _smtpHost;
        private int _smtpPort;
        private string _smtpUsername;
        private string _smtpPassword;
        private SmtpClient _smtpClient;

        public SmtpEmailSender(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword, string smtpFromAddress, ILogger<SmtpEmailSender> logger)
        {
            _smtpFromAddress = smtpFromAddress;
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _logger = logger;

        }

        private async Task EnsureConnectedAsync()
        {
            if (_smtpClient?.IsConnected ?? false) return;

            _smtpClient?.Dispose();
            _smtpClient = new SmtpClient();

            try
            {
                await _smtpClient.ConnectAsync(_smtpHost, _smtpPort, MailKit.Security.SecureSocketOptions.Auto);
                if (!string.IsNullOrWhiteSpace(_smtpUsername) && !string.IsNullOrWhiteSpace(_smtpPassword))
                {
                    _smtpClient.Authenticate(_smtpUsername, _smtpPassword);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to connect or authenticate with SMTP server on {_smtpHost} with port {_smtpPort} {(string.IsNullOrWhiteSpace(_smtpUsername) ? "" : $" using username {_smtpUsername}")}");
                throw;
            }
        }


        public async Task SendEmailAsync(MailQueue mailQueue)
        {
            await EnsureConnectedAsync();

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Connect Asset Manager", _smtpFromAddress));
            emailMessage.To.Add(MailboxAddress.Parse(mailQueue.MailTo));
            emailMessage.Subject = mailQueue.Subject;
            var htmlBody = mailQueue.MailMarkupTemplate.Template.Replace(@"{{body}}", mailQueue.Body);
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };

            using (_smtpClient)
            {
                await _smtpClient.SendAsync(emailMessage);
            }
        }

        public async Task DisconnectAsync()
        {
            if (_smtpClient?.IsConnected ?? false)
            {
                await _smtpClient.DisconnectAsync(true);
                _smtpClient.Dispose();
            }

        }

    }

}
