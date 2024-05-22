using System;
using System.Text.Json.Serialization;
using CabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Entities
{
    public class CabinetStock
    {
        public string CabinetNumber { get; set; }
        [JsonIgnore]
        public Cabinet Cabinet { get; set; }

        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public int MinimalStock { get; set; } = -1;

        public int ActualStock { get; set; }

        public CabinetStockStatus Status { get; set; }

        public DateTime? LastSyncTimeStamp { get; set; }

    }
}
