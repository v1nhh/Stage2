using System;
using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetPositionBaseDTO
    {
        public int ID { get; set; }

        public string CabinetNumber { get; set; }

        public int PositionNumber { get; set; }

        public string PositionAlias { get; set; }

        public PositionType PositionType { get; set; }

        public int CabinetCellTypeID { get; set; }

        public int BladeNo { get; set; }

        public int BladePosNo { get; set; }

        public int? CabinetDoorID { get; set; }

        public DateTime? UpdateDT { get; set; }

        public int MaxNrOfItems { get; set; }

        public bool IsAllocated { get; set; }

        public CabinetPositionStatus Status { get; set; }
    }
}
