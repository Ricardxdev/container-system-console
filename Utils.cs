using System.ComponentModel;
using System.Reflection;

namespace containers
{
    public static class Utils
    {
        public static string GenerateID()
        {
            return "0000000000";
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