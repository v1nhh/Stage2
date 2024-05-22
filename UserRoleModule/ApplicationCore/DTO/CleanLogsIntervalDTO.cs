using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.DTO
{
    public class CleanLogsIntervalDTO
    {
        public LogType LogType { get; set; }
        public int Amount { get; set; }
        public IntervalType IntervalType { get; set; }
    }
}
