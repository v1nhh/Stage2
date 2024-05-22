using CTAMSharedLibrary.Utilities;
using System;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.Utilities
{
    public class ManagementLogUtilities
    {
        public static ManagementLog CreateManagementLog(LogLevel level, LogSource source, string logResourcePath, (string key, string value)[] parameters, CTAMUser user)
        {
            DateTime now = DateTime.UtcNow;
            var logItem = new ManagementLog
            {
                LogDT = now,
                UpdateDT = now,
                Level = level,
                Source = source,
                LogResourcePath = TranslationUtils.GetResourceName(logResourcePath),
                Parameters = TranslationUtils.Serialize(parameters),
                CTAMUserUID = user?.UID,
                CTAMUserEmail = user?.Email,
                CTAMUserName = user?.Name
            };
            return logItem;
        }
    }
}
