using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LinqToFcs.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool HasAnyAttribute(this PropertyInfo propertyInfo, bool inherit)
        {
            return propertyInfo
                .GetCustomAttributes(inherit)
                .Any();
        }

        public static bool HasAnyAttribute<T>(this PropertyInfo propertyInfo, bool inherit)
        {
            return propertyInfo
                .GetCustomAttributes(inherit)
                .OfType<T>()
                .Any();
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this PropertyInfo propertyInfo, bool inherit)
        {
            return propertyInfo
                .GetCustomAttributes(inherit)
                .OfType<T>();
        }

        public static IEnumerable<string> PropertiesToString(this object element, BindingFlags bindingFlag = BindingFlags.Instance | BindingFlags.Public)
        {
            return element
                .GetType()
                .GetProperties(bindingFlag)
                .Select(x => x.GetValue(element).ToString());
        }
    }
}
