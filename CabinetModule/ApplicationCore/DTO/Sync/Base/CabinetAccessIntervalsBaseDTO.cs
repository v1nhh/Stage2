using System;

namespace CabinetModule.ApplicationCore.DTO.Sync.Base
{
    public class CabinetAccessIntervalsBaseDTO
    {
        public int ID { get; set; }

        public int CTAMRoleID { get; set; }

        public int StartWeekDayNr { get; set; }

        public TimeSpan StartTime { get; set; }

        public int EndWeekDayNr { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
