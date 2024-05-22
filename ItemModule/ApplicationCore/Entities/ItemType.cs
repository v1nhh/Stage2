using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.Entities
{
    public class ItemType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public TagType TagType { get; set; }

        public double Depth { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public int MaxLendingTimeInMins { get; set; }

        public bool IsStoredInLocker { get; set; }

        public bool RequiresMileageRegistration { get; set; }

        public ICollection<CTAMRole_ItemType> CTAMRole_ItemType { get; set; }
        public ICollection<ItemType_ErrorCode> ItemType_ErrorCode { get; set; }

        [JsonIgnore]
        public ICollection<Item> Items { get; set; }
    }
}
