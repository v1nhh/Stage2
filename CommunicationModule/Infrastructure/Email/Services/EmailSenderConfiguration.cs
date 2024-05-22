namespace CommunicationModule.Infrastructure.Email.Services
{
    public class EmailSenderConfiguration
    {
        public string SendGridApiKey { get; set; }
        public string MailFromAddress { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public static class Keys
        {
            public const string SendgridAPIKey = "SendgridAPIKey";
            public const string MailFromAddress = "MailFromAddress";
            // Included SmtpFromAddress for backward compatibility
            public const string SmtpFromAddress = "SmtpFromAddress";
            public const string SmtpHost = "SmtpHost";
            public const string SmtpPort = "SmtpPort";
            public const string SmtpUsername = "SmtpUsername";
            public const string SmtpPassword = "SmtpPassword";
            public const string bulk_mail_amount = "bulk_mail_amount";
        }
    }



}