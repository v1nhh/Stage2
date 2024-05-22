using ItemModule.ApplicationCore.Enums;
using System;

namespace ItemModule.ApplicationCore.DTO.Sync.Base
{
    public class ItemSetBaseDTO
    {
        public string SetCode { get; set; }

        public int ItemID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public ItemSetStatus Status { get; set; }
    }
}
