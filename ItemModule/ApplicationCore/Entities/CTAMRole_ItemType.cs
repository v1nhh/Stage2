using System;
using System.Text.Json.Serialization;
using UserRoleModule.ApplicationCore.Entities;

namespace ItemModule.ApplicationCore.Entities
{
    public class CTAMRole_ItemType
    {
        public int CTAMRoleID { get; set; }
        public CTAMRole CTAMRole { get; set; }

        public int ItemTypeID { get; set; }
        [JsonIgnore]
        public ItemType ItemType { get; set; }

        public DateTime CreateDT { get; set; }

        public int MaxQtyToPick { get; set; } = 1;

    }
}
