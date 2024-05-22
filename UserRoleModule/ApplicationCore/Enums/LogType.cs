using CTAMSharedLibrary.Resources;

namespace UserRoleModule.ApplicationCore.Enums
{
    public enum LogType
    {
        Operational = 0,
        Technical = 1,
        Management = 2,
    }

    public static class LogTypeExtension
    {
        public static string GetTranslation(this LogType logType)
        {
            return CloudTranslations.ResourceManager.GetString($"logs.logType.{logType.ToString().ToLower()}");
        }
    }
}
