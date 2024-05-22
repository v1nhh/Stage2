using MileageModule.ApplicationCore.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace MileageModule.ApplicationCore.Entities
{
    public class MileageRegistration
    {
        [Key]
        public int ID { get; set; }

        public int MileageID { get; set; }
        public Mileage Mileage { get; set; }

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

        public DateTime? LastSyncTimeStamp { get; set; }
    }
}
