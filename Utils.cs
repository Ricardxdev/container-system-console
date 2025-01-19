using System.ComponentModel;
using System.Reflection;

namespace containers
{
    public static class Utils
    {
        public static string GenerateID()
        {
            //Code of 3 letter plus 5 digits
            //Example: ABC12345
            uint numberOfLetters = 3;
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            uint numberOfDigits = 5;
            string numbers = "0123456789";

            string code = "";
            Random random = new Random();
            for (uint i = 0; i < numberOfLetters; i++)
            {
                code += letters[random.Next(0, letters.Length)];
            }

            random = new Random();
            for (uint i = 0; i < numberOfDigits; i++) {
                code += numbers[random.Next(0, numbers.Length)];
            }
            
            return code;
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return value.ToString();
            }
            DescriptionAttribute[]? attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return value.ToString();
        }
    }
}