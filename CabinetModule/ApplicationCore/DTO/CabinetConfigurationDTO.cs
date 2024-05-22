using System.Collections.Generic;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetConfigurationDTO
    {
        public class Position
        {
            public int ID { get; set; }
            public int BladeAddr { get; set; }
            public int BladePosNo { get; set; }
            public string Alias { get; set; }
            public string PositionType { get; set; }
            public int DoorID { get; set; }
            public int NodeLF { get; set; }
            public int NodeHF { get; set; }
        }

        public class CabinetConfiguration
        {
            public int NrBlades { get; set; }
            public string KCType { get; set; }
            public bool UseLFReader { get; set; }
            public bool UseHFReader { get; set; }
            public List<Position> Positions { get; set; }
        }
    }
}
