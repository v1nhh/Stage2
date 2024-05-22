using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CTAM.Core.Enums;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class CTAMPermission
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public CTAMModule CTAMModule { get; set; }

        [JsonIgnore]
        public ICollection<CTAMRole_Permission> CTAMRole_Permission { get; set; }
    }
}
