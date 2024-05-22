using System;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetPositionContentBaseDTO
    {
        public int CabinetPositionID { get; set; }

        public int ItemID { get; set; }

        public DateTime CreateDT { get; set; }
    }
}
