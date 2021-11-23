using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shareds.Mapping.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class MapperExtensions
    {
        /// <summary>
        /// Returns true if type is IEnumerable<> or ICollection<>, IList<> ...
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(this Type type)
        {
            if (!type.IsGenericType)
                // Return false, If not types of ICollection<>, IList<> ...
                return false;
            if (!type.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable)))
                // Return false, If not types of IEnumerable<>.
                return false;

            return true;
        }
        /// <summary>
        /// Creates an instance of the specified type using that type's default constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Create(this Type type)
        {
            if (type.IsInterface)
                // Throw error messages, If types of Interface.
                throw new Exception("don't know any implementation of this type: " + type.Name);

            return !type.IsEnumerable()
                ? Activator.CreateInstance(type)
                : Activator.CreateInstance(typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]));
        }
    }
}
