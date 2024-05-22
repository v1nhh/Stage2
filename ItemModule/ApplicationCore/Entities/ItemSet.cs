using System;
using System.Text.Json.Serialization;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.Entities
{
    public class ItemSet
    {
        public string SetCode { get; set; }

        public int ItemID { get; set; }
        [JsonIgnore]
        public Item Item { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public ItemSetStatus Status { get; set; }
    }
}
