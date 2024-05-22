using System;
using System.Reflection;

namespace CTAM.Core.Utilities
{
    public class EnvironmentUtils
    {
        public static string GetEnvironmentVariable(string environmentName)
        {
            var environmentValue = Environment.GetEnvironmentVariable(environmentName);
            if (environmentValue == null)
            {
                throw new ArgumentNullException($"Environment variable {environmentName} is not defined");
            }
            return environmentValue;
        }

        /// <summary>
        /// Gets `Version` value defined in .csproj file of the passed assembly
        /// </summary>
        /// <param name="assembly"></param> e.g. `Assembly.GetEntryAssembly()`
        /// <returns></returns>
        public static string GetVersion(Assembly assembly)
        {
            Version version = assembly.GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public static string MaskConnectionStringPassword(string connectionString)
        {
            var parts = connectionString.Split(',', ';');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].StartsWith("password=", StringComparison.OrdinalIgnoreCase))
                {
                    parts[i] = "password=******";
                }
            }
            return string.Join(";", parts);
        }

    }
}
