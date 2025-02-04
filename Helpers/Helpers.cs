using System.ComponentModel;
using System.Reflection;
using containers.Models;

namespace containers.Helpers
{
    public static class Helpers
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
            for (uint i = 0; i < numberOfDigits; i++)
            {
                code += numbers[random.Next(0, numbers.Length)];
            }

            return code;
        }

        public static bool ValidateNumeric(string str)
        {
            foreach (char c in str)
            {
                if (c - '0' < 0 || c - '0' > 9) return false;
            }

            return true;
        }

        public static bool ValidateFloat(string str)
        {
            int dotCount = 0;
            foreach (char c in str)
            {
                if (c == '.') dotCount++;
                else if (c - '0' < 0 || c - '0' > 9) return false;
            }

            if (dotCount > 1) return false;

            return true;
        }
    }

    public static class EnumExtensions
    {
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
        
        public static string ToCustomString(this ContainerType type)
        {
            return type switch
            {
                ContainerType.TEU => "TEU",
                ContainerType.FEU => "FEU",
                ContainerType.HC => "HC",
                ContainerType.RF => "RF",
                ContainerType.OT => "OT",
                ContainerType.FR => "FR",
                ContainerType.TC => "TC",
                _ => "Unknown Type"
            };
        }

        public static string ToCustomString(this ContainerState state)
        {
            return state switch
            {
                ContainerState.IN_DEPOSIT => "IN_DEPOSIT",
                ContainerState.CHARGED => "CHARGED",
                ContainerState.IN_TRANSIT => "IN_TRANSIT",
                ContainerState.DISCHARGED => "DISCHARGED",
                ContainerState.DELIVERED => "DELIVERED",
                ContainerState.IN_MAINTENANCE => "IN_MAINTENANCE",
                ContainerState.EMPTY => "EMPTY",
                _ => "Unknown State"
            };
        }
    }
}