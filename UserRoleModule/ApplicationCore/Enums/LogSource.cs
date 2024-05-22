namespace UserRoleModule.ApplicationCore.Enums
{
    public enum LogSource
    {
        CabinetUI = 0,
        HardwareAPI = 1,
        LocalAPI = 2,
        CloudAPI = 3,

        // Zijn er nog meer modules die een log wegschrijven in de logtabellen?

        Other = 10
    }
}
