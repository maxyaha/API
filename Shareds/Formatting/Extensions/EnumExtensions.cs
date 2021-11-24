using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Shareds.Formatting.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();

            MemberInfo[] memberInfo = type.GetMember(@enum.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if ((attribs != null && attribs.Any()))
                    return ((DescriptionAttribute)attribs.ElementAt(0)).Description;
            }
            return @enum.ToString();
        }
    }
}
