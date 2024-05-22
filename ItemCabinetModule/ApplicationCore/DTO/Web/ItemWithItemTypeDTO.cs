using System.Collections.Generic;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO.Web
{
    public class ItemWithItemTypeDTO
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public int ItemTypeID { get; set; }

        public ItemTypeDTO ItemType { get; set; }

        public string ExternalReferenceID { get; set; }

        public string Barcode { get; set; }

        public string Tagnumber { get; set; }

        public int? ErrorCodeID { get; set; }

        public ErrorCodeDTO ErrorCode { get; set; }

        public int MaxLendingTimeInMins { get; set; }

        public int NrOfSubItems { get; set; }

        public bool AllowReservations { get; set; }

        public ItemStatus Status { get; set; }

        public ICollection<ItemDetailDTO> ItemDetails { get; set; }

    }
}
