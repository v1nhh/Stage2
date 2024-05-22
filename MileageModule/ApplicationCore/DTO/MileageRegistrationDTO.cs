using MileageModule.ApplicationCore.Enums;
using System;

namespace MileageModule.ApplicationCore.DTO
{
    public class MileageRegistrationDTO
    {
        public int ID { get; set; }

        public int MileageID { get; set; }

        public MileageDTO Mileage { get; set; }

        public string CTAMUserUID { get; set; }

        public string CTAMUserName { get; set; }

        public string CTAMUserEmail { get; set; }


        public DateTime CreateDT { get; set; }

        public int UserMileage { get; set; }

        public int ValidatedMileage { get; set; }

        public string ValidatedByCTAMUserUID { get; set; }

        public string ValidatedByCTAMUserName { get; set; }

        public string ValidatedByCTAMUserEmail { get; set; }

        public DateTime? ValidatedOnDT { get; set; }

        public MileageRegistrationStatus Status { get; set; }
    }
}
