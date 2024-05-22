using CabinetModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetCreateDTO
    {
        public string CabinetNumber { get; set; }

        public string Name { get; set; }

        public CabinetType CabinetType { get; set; }

        public string Description { get; set; }

        public LoginMethod LoginMethod { get; set; }

        public string LocationDescr { get; set; }

        public string CabinetConfiguration { get; set; }

        public string Email { get; set; }

        public CabinetStatus Status { get; set; } 

        public int NumberOfPositions { get; set; }

        public int CabinetCellTypeID { get; set; }
    }
}
