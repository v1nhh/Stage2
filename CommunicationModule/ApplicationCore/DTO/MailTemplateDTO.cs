using System;

namespace CommunicationModule.ApplicationCore.DTO
{
    public class MailTemplateDTO
    {
        public int ID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public string Name { get; set; }

        public string TemplateNL { get; set; }

        public string TemplateEN { get; set; }
    }
}
