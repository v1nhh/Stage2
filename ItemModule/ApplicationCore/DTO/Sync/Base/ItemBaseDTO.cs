using System;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.DTO.Sync.Base
{
    public class ItemBaseDTO
    {
        public int ID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public string Description { get; set; }

        public int ItemTypeID { get; set; }

        public string ExternalReferenceID { get; set; }

        public string Barcode { get; set; }

        public string Tagnumber { get; set; }

        public int? ErrorCodeID { get; set; }

        public int MaxLendingTimeInMins { get; set; }

        public int NrOfSubItems { get; set; }

        public bool AllowReservations { get; set; }

        public ItemStatus Status { get; set; }
    }
}
