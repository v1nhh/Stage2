using System;
using ItemModule.ApplicationCore.Enums;

namespace ItemModule.ApplicationCore.DTO
{
    public class ItemSetDTO
    {
        public string SetCode { get; set; }

        public int ItemID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public ItemSetStatus Status { get; set; }
    }
}
