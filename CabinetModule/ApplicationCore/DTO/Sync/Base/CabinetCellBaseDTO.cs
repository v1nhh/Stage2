using System;

namespace CabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetCellBaseDTO
    {
        public int ID { get; set; }

        public int CabinetColumnID { get; set; }

        public int CabinetCellTypeID { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
