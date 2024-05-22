using System;

namespace CabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetUIBaseDTO
    {
        public string CabinetNumber { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public string LogoWhite { get; set; }

        public string LogoBlack { get; set; }

        public string ColorTemplate { get; set; }

        public string MenuTemplate { get; set; }

        public string Font { get; set; }
    }
}
