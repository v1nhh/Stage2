using System;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetPropertiesDTO
    {
        public string CabinetNumber { get; set; }

        public string LocalApiVersion { get; set; }

        public string LocalUiVersion { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }
    }
}
