using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Entities
{
    public class CTAMUserPersonalItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CTAMUserUID { get; set; }
        [JsonIgnore]
        public CTAMUser CTAMUser { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int? ReplacementItemID { get; set; }
        public Item ReplacementItem { get; set; }

        public string CabinetNumber { get; set; }
        public string CabinetName { get; set; }

        public UserPersonalItemStatus Status { get; set; }

        public DateTime? LastSyncTimeStamp { get; set; }

    }
}
