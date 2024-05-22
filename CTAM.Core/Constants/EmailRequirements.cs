using CTAM.Core.Enums;
using System;

namespace CTAM.Core.Constants
{
    public class EmailRequirements
    {
        private static readonly string MatchPasswordPatternLow =    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W\D_]).{6,}$";
        private static readonly string MatchPasswordPatternMedium = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W\D_]).{12,}$";
        private static readonly string MatchPasswordPatternHigh =   @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W\D_]).{20,}$";
        public static (string regex, int length) GetPasswordRegex(string ctamSetting)
        {
            var policy = ToPasswordPolicy(ctamSetting);

            return policy switch
            {
                PasswordPolicy.Low => (MatchPasswordPatternLow, 6),
                PasswordPolicy.Medium => (MatchPasswordPatternMedium, 12),
                PasswordPolicy.High => (MatchPasswordPatternHigh, 20),
                _ => (MatchPasswordPatternHigh, 20)
            };
        }
        public static int GetPasswordMinimalLength(string ctamSetting)
        {
            var policy = ToPasswordPolicy(ctamSetting);

            return policy switch
            {
                PasswordPolicy.Low => 6,
                PasswordPolicy.Medium => 12,
                PasswordPolicy.High => 20,
                _ => 20
            };
        }

        private static PasswordPolicy ToPasswordPolicy(string val)
        {
            if (Enum.TryParse<PasswordPolicy>(val, out PasswordPolicy policy))
            {
                return policy;
            }

            return PasswordPolicy.High;
        }
    }
}
