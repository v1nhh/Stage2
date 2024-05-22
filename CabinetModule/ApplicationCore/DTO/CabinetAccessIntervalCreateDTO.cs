using System;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetAccessIntervalCreateDTO
    {
        public int StartWeekDayNr { get; set; }

        public TimeSpan StartTime { get; set; }

        public int EndWeekDayNr { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
