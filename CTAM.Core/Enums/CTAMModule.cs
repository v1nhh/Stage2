using System;

namespace CTAM.Core.Enums
{
    public enum CTAMModule
    {
        Management = 0,
        Cabinet = 1,
    }

    public static class CTAMModuleExtension
    {

        public static string GetName(this CTAMModule module)
        {
            return Enum.GetName(typeof(CTAMModule), module);
        }
    }
}