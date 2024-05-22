using CabinetModule.ApplicationCore.Enums;
using System;
namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetDoorDTO
    {
        public int ID { get; set; }

        public string Alias { get; set; }

        public string CabinetNumber { get; set; }

        public CabinetDoorStatus Status { get; set; }

        public int GPIOPortDoorState { get; set; } // should be enum?

        public bool ClosedLevel { get; set; }

        public int GPIOPortDoorControl { get; set; } // should be enum?

        public bool UnlockLevel { get; set; }

        public int UnlockDuration { get; set; }

        public int MaxOpenDuration { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
