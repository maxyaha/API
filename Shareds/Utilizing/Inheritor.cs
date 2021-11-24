using System;
using System.Linq;
using System.Reflection;

namespace Shareds.Utilizing
{
    public static class Inheritor
    {
        public static void FillProperties<Target, TBase>(this Target deliver, TBase super) where Target : TBase
        {
            Type source = typeof(TBase);
            Type target = typeof(Target);

            var properties = source.GetProperties();
            // SKIP UNREADABLE AND WRITEPROTECTED ONES
            Func<PropertyInfo, bool> predicate = o
                => o.CanRead
                && o.CanWrite;
            foreach (var property in properties.Where(predicate))
            {
                // READ VALUE
                var value = property.GetValue(super, null);
                // GET PROPERTY OF TARGET CLASS, WRITE VALUE TO TARGET
                target.GetProperty(property.Name).SetValue(deliver, value, null);
            }
        }
    }
}
