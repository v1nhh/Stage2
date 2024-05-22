using CommunicationModule.ApplicationCore.Enums;
using System;

namespace CommunicationModule.ApplicationCore.DTO
{
    public class MailQueueDTO
    {
        public int ID { get; set; }

        public DateTime CreateDT { get; set; }

        public string MailTo { get; set; }

        public string MailCC { get; set; }

        public bool Prio { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string Reference { get; set; }

        public int FailedAttempts { get; set; }

        public string LastFailedErrorMessage { get; set; }

        public MailQueueStatus Status { get; set; }
    }
}
