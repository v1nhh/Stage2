using ReservationModule.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReservationModule.ApplicationCore.DTO
{
    public class ReservationRecurrencyScheduleDTO
    {
        public int ID { get; set; }

        public RecurrenceFrequency RecurrenceFrequency { get; set; }

        public int? Interval { get; set; }

        public int? DayNumber { get; set; }

        public bool? Sunday { get; set; }

        public bool? Monday { get; set; }

        public bool? Tuesday { get; set; }

        public bool? Wednesday { get; set; }

        public bool? Thursday { get; set; }

        public bool? Friday { get; set; }

        public bool? Saturday { get; set; }

        public int? WeekOfMonth { get; set; }

        public int? MonthOfYear { get; set; }

        public DateTime EndDateTime { get; set; }

        [JsonIgnore]
        public ICollection<ReservationDTO> Reservations { get; set; }
    }
}
