using System;
using System.Text.Json.Serialization;

namespace UserRoleModule.ApplicationCore.Entities
{
    public class CTAMUser_Role
    {
        public DateTime CreateDT { get; set; }

        public string CTAMUserUID { get; set; }

        [JsonIgnore]
        public CTAMUser CTAMUser { get; set; }

        public int CTAMRoleID { get; set; }

        [JsonIgnore]
        public CTAMRole CTAMRole { get; set; }

    }
}
