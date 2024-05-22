using System;
using CabinetModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Entities;

namespace ItemCabinetModule.ApplicationCore.Entities
{
    public class AllowedCabinetPosition
    {
        public int ItemID { get; set; }
        public Item Item { get; set; }

        public int CabinetPositionID { get; set; }
        public CabinetPosition CabinetPosition { get; set; }

        public bool IsBaseCabinetPosition { get; set; }

        public DateTime CreateDT { get; set; }
    }
}
