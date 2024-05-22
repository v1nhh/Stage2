using System;
using System.ComponentModel.DataAnnotations;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.DTO
{
    public class ItemWebDTO
    {
        [Key]
        public int ID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        [Required]
        public string Description { get; set; }

        public int ItemTypeID { get; set; }
        public ItemType ItemType { get; set; }

        public string ExternalReferenceID { get; set; }

        public string Barcode { get; set; }

        public string Tagnumber { get; set; }

        public int? ErrorCodeID { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public int MaxLendingTimeInMins { get; set; }

        public int NrOfSubItems { get; set; }

        public bool AllowReservations { get; set; }

        public ItemStatus Status { get; set; }

        public ItemSet ItemSet { get; set; }

        public DateTime? LastSyncTimeStamp { get; set; }

        public bool IsChecked { get; set; }
    }
}
