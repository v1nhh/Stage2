using System;
namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetAccessIntervalDTO
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        public int StartWeekDayNr { get; set; }

        public TimeSpan StartTime { get; set; }

        public int EndWeekDayNr { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
