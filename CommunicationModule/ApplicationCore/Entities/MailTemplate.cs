using System;
using System.ComponentModel.DataAnnotations;

namespace CommunicationModule.ApplicationCore.Entities
{
    public class MailTemplate
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Template { get; set; }

        [Required]
        public string LanguageCode { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
