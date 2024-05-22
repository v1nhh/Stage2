using System;
using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO
{
    public class UserPersonalItemDTO
    {
        public int ID { get; set; }

        public string CTAMUserUID { get; set; }

        public int ItemID { get; set; }

        public int? ReplacementItemID { get; set; }

        public string CabinetNumber { get; set; }

        public string CabinetName { get; set; }

        public UserPersonalItemStatus Status { get; set; }

        public DateTime? UpdateDT { get; set; }

    }
}
