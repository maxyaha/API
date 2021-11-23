using Omu.ValueInjecter.Injections;
using System;
using System.Reflection;

namespace Shareds.Mapping.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ConverterExtensions : LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            return (source.Name.Equals(target.Name))
                && (!source.IsValueType)
                && (!source.Equals(typeof(string)))
                && (!source.IsGenericType)
                && (!target.IsGenericType);
                //|| (source.IsEnumerable())
                //&& (target.IsEnumerable());
        }


        /// <summary>
        /// 
        /// </summary>
        internal class Int32 : LoopInjection
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="convention"></param>
            /// <returns></returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source.Name.Equals(target.Name)
                    && source.IsSubclassOf(typeof(System.Enum))
                    && target.Equals(typeof(int));

            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal class Enum : LoopInjection
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="convention"></param>
            /// <returns></returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source.Name.Equals(target.Name)
                    && source.Equals(typeof(int))
                    && target.IsSubclassOf(typeof(System.Enum));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal class Nullables : LoopInjection
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="convention"></param>
            /// <returns></returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source.Name.Equals(target.Name)
                    && source.Equals(Nullable.GetUnderlyingType(target));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal class Normal : LoopInjection
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="convention"></param>
            /// <returns></returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source.Name.Equals(target.Name)
                    && target.Equals(Nullable.GetUnderlyingType(source));
            }
        }
    }
}
