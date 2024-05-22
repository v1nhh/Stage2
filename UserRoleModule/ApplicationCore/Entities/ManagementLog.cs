using System;
using System.ComponentModel.DataAnnotations;
using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class ManagementLog
    {
        [Key]
        public int ID { get; set; }

        public DateTime LogDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public LogLevel Level { get; set; }

        public LogSource Source { get; set; }

        [Required]
        public string LogResourcePath { get; set; }

        public string Parameters { get; set; }

        public string CTAMUserUID { get; set; }

        public string CTAMUserName { get; set; }

        public string CTAMUserEmail { get; set; }
    }
}
