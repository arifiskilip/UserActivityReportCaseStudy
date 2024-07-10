using System.Text;

namespace Application.Helpers
{
    public static class PasswordHelper
    {
        public static string RandomPasswordGenerator()
        {
            Random random = new();
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            // Ensure we have at least one character of each type
            var password = new StringBuilder();
            password.Append(upperChars[random.Next(upperChars.Length)]);
            password.Append(lowerChars[random.Next(lowerChars.Length)]);
            password.Append(digitChars[random.Next(digitChars.Length)]);
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Fill the remaining characters with a mix of all types
            const string allChars = upperChars + lowerChars + digitChars + specialChars;
            for (int i = password.Length; i < 6; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the password to ensure randomness
            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }
    }
}
