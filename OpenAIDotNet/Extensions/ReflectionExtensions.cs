using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIDotNet.Extensions
{
    public static class ReflectionExtensions
    {
        public static Dictionary<string, T> ObjectPropertyValuesAs<T>(this object obj)
        {
            var type = obj.GetType();
            var props = type.GetProperties();
            var result = props.ToDictionary(x => x.Name, x => (T)x.GetValue(obj)!);
            return result;
        }
        public static Dictionary<string, string?> ObjectPropertyStringValues(this object obj)
        {
            var type = obj.GetType();
            var props = type.GetProperties();
            var result = props.ToDictionary(x => x.Name, x => x.GetValue(obj)?.ToString());
            return result;
        }
        public static string ToEnumDescription<T>(this T value) where T : Enum
        {
            var fi = value.GetType().GetField(value.ToString());


            if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
