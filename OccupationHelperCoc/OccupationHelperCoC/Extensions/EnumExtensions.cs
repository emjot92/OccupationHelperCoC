using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OccupationHelperCoC.Extensions
{
    public static class EnumExtensions
    {
        public static T GetValueFromDescription<T>(this string description) where T : Enum
        {
            Type type = typeof(T);

            foreach (System.Reflection.FieldInfo field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException($"Description {description} not found in enum {type.Name}");
        }

        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            string name = Enum.GetName(type, @enum);

            if (name == null)
            {
                if (type.IsDefined(typeof(FlagsAttribute), false))
                {
                    var stringBuilder = new StringBuilder();
                    foreach (Enum enumValue in Enum.GetValues(type).Cast<Enum>().Where(x => Convert.ToUInt64(x) != 0))
                    {
                        if (@enum.HasFlag(enumValue))
                        {
                            stringBuilder.AppendLine(enumValue.GetDescription());
                        }
                    }
                    return stringBuilder.ToString();
                }
                return null;
            }

            System.Reflection.FieldInfo field = type.GetField(name);
            if (field == null)
            {
                return null;
            }

            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attr == null)
            {
                return null;
            }

            return attr.Description;
        }

        public static IEnumerable<T> GetFlags<T>(this T @enum) where T : Enum
        {
            return Enum.GetValues(@enum.GetType()).Cast<T>().Where(t => Convert.ToUInt64(t) != 0).Where(x => @enum.HasFlag(x)).Cast<T>();
        }
    }
}
