using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UserRoleModule.ApplicationCore.Entities;

namespace CabinetModule.ApplicationCore.Entities
{
    public class CabinetAccessInterval
    {
        [Key]
        public int ID { get; set; }

        public int CTAMRoleID { get; set; }

        public int StartWeekDayNr { get; set; }

        public TimeSpan StartTime { get; set; }

        public int EndWeekDayNr { get; set; }

        public TimeSpan EndTime { get; set; }



        [JsonIgnore]
        public CTAMRole CTAMRole { get; set; }
    }
}
