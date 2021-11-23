using System;

namespace Shareds.Utilizing
{
    /// <summary>
    /// 
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Action<object> Convert<T>(Action<T> action)
        {
            if (action is null)
                //  The value is null.
                return null;

            return new Action<object>(o => action((T)o));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static dynamic ChangeTo(dynamic source, Type type)
        {
            return System.Convert.ChangeType(source, type);
        }
    }
}
