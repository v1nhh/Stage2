using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO.CabinetStock
{
    public class CabinetStockDTO
    {
        public string CabinetNumber { get; set; }

        public int ItemTypeID { get; set; }
        public string ItemTypeDescription { get; set; }

        public int MinimalStock { get; set; }

        public int ActualStock { get; set; }

        public CabinetStockStatus Status { get; set; }

    }
}
