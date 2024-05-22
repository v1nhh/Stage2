using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class CTAMRole
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? ValidFromDT { get; set; }

        public DateTime? ValidUntilDT { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        [JsonIgnore]
        public ICollection<CTAMUser_Role> CTAMUser_Roles { get; set; }

        public ICollection<CTAMRole_Permission> CTAMRole_Permission { get; set; }
    }
}
