using System;
using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetStockBaseDTO
    {
        public string CabinetNumber { get; set; }

        public int ItemTypeID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public int MinimalStock { get; set; } = -1;

        public int ActualStock { get; set; }

        public CabinetStockStatus Status { get; set; }
    }
}
