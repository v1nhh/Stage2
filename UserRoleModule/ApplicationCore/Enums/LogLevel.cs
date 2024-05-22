using CTAMSharedLibrary.Resources;
namespace UserRoleModule.ApplicationCore.Enums
{
    public enum LogLevel
    {
        Fatal = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Debug = 4,
    }

    public static class LogLevelExtension
    {

        public static string GetTranslation(this LogLevel level)
        {
            return CloudTranslations.ResourceManager.GetString($"logs.logLevel.{level.ToString().ToLower()}");
        }
    }
}
