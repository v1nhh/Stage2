using CommunicationModule.ApplicationCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommunicationModule.ApplicationCore.Entities
{
    public class MailQueue
    {
        [Key]
        public int ID { get; set; }

        public int MailMarkupTemplateID { get; set; }

        public DateTime CreateDT { get; set; }

        [Required]
        public string MailTo { get; set; }

        public string MailCC { get; set; }

        public bool Prio { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Reference { get; set; }

        public MailQueueStatus Status { get; set; }

        public int FailedAttempts { get; set; }

        public string LastFailedErrorMessage { get; set; }

        [JsonIgnore]
        public MailMarkupTemplate MailMarkupTemplate { get; set; }
    }
}
