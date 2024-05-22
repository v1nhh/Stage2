using System;
namespace CabinetModule.ApplicationCore.Utilities
{
    public static class Kcid
    {
        public static string NewKcid()
        {
            Int32 timestampKCID = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            DateTime dtKCID = new DateTime(1970, 1, 1).AddSeconds(timestampKCID).ToUniversalTime();
            return dtKCID.ToLocalTime().ToString("yyMMddHHmmss");
        }
    }
}
