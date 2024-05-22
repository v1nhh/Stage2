using System;
using System.ComponentModel.DataAnnotations;
using UserRoleModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetLog
    {
        [Key]
        public int ID { get; set; }

        public DateTime LogDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public LogLevel Level { get; set; }

        [Required]
        public string CabinetNumber { get; set; }

        public string CabinetName { get; set; }

        public LogSource Source { get; set; }

        [Required]
        public string LogResourcePath { get; set; }

        public string Parameters { get; set; }

    }
}
