using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Shareds.Formatting
{
    public static class DictionaryFormatter
    {
        public static T ToObject<T>(this IDictionary<string, string> dict) where T : new()
        {
            var @object = new T();
            PropertyInfo[] properties = @object.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                KeyValuePair<string, string> pair = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                // FIND WHICH PROPERTY TYPE (INT, STRING, DOUBLE? ETC) THE CURRENT PROPERTY IS
                // FIX NULLABLES
                // AND CHANGE THE TYPE
                Type type = null;
                type = property.PropertyType;
                type = Nullable.GetUnderlyingType(type) 
                    ?? type;
                object value = Convert.ChangeType(pair.Value, type);
                property.SetValue(@object, value, null);
            }
            return @object;
        }


        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source is null)
                throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);

            if (value is T)
                dictionary.Add(property.Name, (T)value);
        }
    }
}
