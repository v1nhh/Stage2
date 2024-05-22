using System;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetUI
    {
        //[Key]
        //public int ID { get; set; }
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
