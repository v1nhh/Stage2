using System;
using ItemCabinetModule.ApplicationCore.Enums;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CTAMUserPersonalItemBaseDTO
    {
        public int ID { get; set; }

        public string CTAMUserUID { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public int ItemID { get; set; }

        public int? ReplacementItemID { get; set; }

        public string CabinetNumber { get; set; }
        public string CabinetName { get; set; }

        public UserPersonalItemStatus Status { get; set; }
    }
}
