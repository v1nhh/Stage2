using System;
using System.ComponentModel.DataAnnotations;

namespace CommunicationModule.ApplicationCore.Entities
{
    public class MailMarkupTemplate
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Template { get; set; }
    }
}
