using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Entities
{
    public class ItemToPick
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string CTAMUserUID { get; set; }
        [JsonIgnore]
        public CTAMUser CTAMUser { get; set; }

        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int CabinetPositionID { get; set; }
        public CabinetPosition CabinetPosition { get; set; }

        public ItemToPickStatus Status { get; set; }

        public DateTime? LastSyncTimeStamp { get; set; }
    }
}
