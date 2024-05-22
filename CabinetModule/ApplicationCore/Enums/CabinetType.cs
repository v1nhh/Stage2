namespace CabinetModule.ApplicationCore.Enums
{
    public enum CabinetType
    {
        CombiLocker = 0,
        KeyConductor = 1,
        Locker = 3,
    }

    public static class CabinetTypeExtension
    {

        public static string GetHardwareAPIType(this CabinetType cabinetType)
        {
            return cabinetType switch
            {
                CabinetType.CombiLocker => "combilocker",
                CabinetType.KeyConductor => "keyconductor",
                CabinetType.Locker => "locker",
                _ => "unknown"
            };
        }
    }
}
