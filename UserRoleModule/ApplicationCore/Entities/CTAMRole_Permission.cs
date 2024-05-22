using System;
using System.Text.Json.Serialization;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class CTAMRole_Permission
    {
        public int CTAMRoleID { get; set; }
        [JsonIgnore]
        public CTAMRole CTAMRole { get; set; }

        public int CTAMPermissionID { get; set; }
        public CTAMPermission CTAMPermission { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
