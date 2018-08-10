using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MVCValidation.Helpers
{
    public static class GetDisplayNameAttribute
    {
        public static string Value(object item, string name)
        {
            MemberInfo property = item.GetType().GetProperty(name);
            var displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().SingleOrDefault();
            if (displayNameAttribute == null)
            {
                return name;
            }
            return displayNameAttribute.DisplayName;
        }
    }
}