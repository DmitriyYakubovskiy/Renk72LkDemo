using System.ComponentModel;
using System.Reflection;

namespace Renk72Lk.DataAccess.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0) return attributes[0].Description;

        return value.ToString();
    }

    public static int? GetEnumValueFromDescription(string description, Type enumType)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException("Тип должен быть enum");

        var fields = enumType.GetFields();
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (attribute != null && attribute.Description == description)
            {
                return (int)field.GetValue(null);
            }
        }

        return null;
    }
}
