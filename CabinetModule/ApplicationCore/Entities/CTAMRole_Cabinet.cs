using System;
using System.Text.Json.Serialization;
using UserRoleModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CTAMRole_Cabinet
    {
        public string CabinetNumber { get; set; }

        public int CTAMRoleID { get; set; }
        
        public DateTime CreateDT { get; set; }



        public Cabinet Cabinet { get; set; }

        [JsonIgnore]
        public CTAMRole CTAMRole { get; set; }
    }
}
