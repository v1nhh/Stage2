using System;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class AllowedCabinetPositionBaseDTO
    {
        public int ItemID { get; set; }

        public int CabinetPositionID { get; set; }

        public bool IsBaseCabinetPosition { get; set; }

        public DateTime CreateDT { get; set; }
    }
}
