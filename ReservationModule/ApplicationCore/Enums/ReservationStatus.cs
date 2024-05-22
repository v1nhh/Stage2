namespace ReservationModule.ApplicationCore.Enums
{
    public enum ReservationStatus
    {
        Created = 0,
        ReminderSent = 1,
        FinalReminderSent = 2,
        Cancelled = 3,
        Picked = 4,
        Returned = 5,
        OverdueWarningSent = 6,
        Overdue = 7
    }
}
