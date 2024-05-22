using ReservationModule.ApplicationCore.Enums;
using System;

namespace ReservationModule.ApplicationCore.DTO
{
    public class ReservationDTO
    {
        public int ID { get; set; }

        public string CTAMUserUID { get; set; }

        public string CTAMUserName { get; set; }

        public string CTAMUserEmail { get; set; }

        public string QRCode { get; set; }

        public ReservationType ReservationType { get; set; }

        public DateTime StartDT { get; set; }

        public DateTime? EndDT { get; set; }

        public string NoteForUser { get; set; }

        public ReservationStatus Status { get; set; }

        public DateTime? TakeDT { get; set; }

        public DateTime? PutDT { get; set; }

        public bool IsAdhoc { get; set; }

        public int? ReservationRecurrencyScheduleID { get; set; }
        public ReservationRecurrencyScheduleDTO ReservationRecurrencySchedule { get; set; }

        public string ExternalReservationNumber { get; set; }

        public string ExternalReservationSourceType { get; set; }

        public string ExternalReservationCallBackInfo { get; set; }

        public DateTime CreateDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public DateTime? CancelledDT { get; set; }

        public string CancelledByCTAMUserUID { get; set; }
        public string CancelledByCTAMUserName { get; set; }

        public string CancelledByCTAMUserEmail { get; set; }

    }
}
