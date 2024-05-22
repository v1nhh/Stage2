using System;
using System.ComponentModel.DataAnnotations;
using CTAM.Core.Enums;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class CTAMSetting
    {
        [Key]
        public int ID { get; set; }

        public CTAMModule CTAMModule { get; set; }

        [Required]
        public string ParName { get; set; }

        public string ParValue { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

    }
}
