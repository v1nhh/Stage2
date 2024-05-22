using System;

namespace CloudAPI.Utilities
{
    public class PasswordUtilities
    {
        private static Random _random = new();

        public static string GenerateRandomPassword(int length)
        {
            var newPassword = Guid.NewGuid().ToString().Substring(0, length - 4);
            char capital = (char)_random.Next(65, 90); // A-Z
            char lower = (char)_random.Next(97, 122); // a-z
            char special = (char)_random.Next(33, 43); // !"#$%&'()*+
            char digit = (char)_random.Next(48, 57); // 0-9

            return ShuffleString(newPassword + capital + lower + special + digit);

        }
        public static string ShuffleString(string input)
        {
            var charArray = input.ToCharArray();

            // shuffle the characters using Fisher-Yates algorithm
            for (int i = charArray.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                char temp = charArray[i];
                charArray[i] = charArray[j];
                charArray[j] = temp;
            }

            return new string(charArray);
        }
    }
}
