using System;

namespace MileageModule.ApplicationCore.DTO
{
    public class MileageDTO
    {
        public int ID { get; set; }

        public int ItemID { get; set; }

        public DateTime CreateDT { get; set; }

        public string LicensePlate { get; set; }

        public int CurrentMileage { get; set; }

        public int MaxDeltaMileage { get; set; }

        public int ServiceMileage { get; set; }

        public string UoM { get; set; }
    }
}
