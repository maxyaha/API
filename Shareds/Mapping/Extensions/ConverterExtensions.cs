using Newtonsoft.Json.Linq;
using Omu.ValueInjecter.Injections;
using System;
using System.Reflection;

namespace Shareds.Mapping.Extensions
{
    /// <summary>
    /// Custom injections by ValueInjecter(https://github.com/omuleanu/ValueInjecter/wiki/custom-injections-examples)
    /// </summary>
    public class ConverterExtensions : LoopInjection
    {
        protected override bool MatchTypes(Type source, Type target)
        {
            return (source.Name.Equals(target.Name))
                && (!source.IsValueType)
                && (!source.Equals(typeof(string)))
                && (!source.Equals(typeof(JContainer)))
                && (!source.IsGenericType)
                && (!target.IsGenericType);
             

        }

        protected override void SetValue(object source, object target, PropertyInfo sp, PropertyInfo tp)
        {
            if (sp.GetValue(source) is null)
                //  The value is null.
                tp.SetValue(target, sp.GetValue(source));
            else if (sp.Name != tp.Name)
                // The name is not interrelated.
                tp.SetValue(target, sp.GetValue(source));
            else
                tp.SetValue(target, Mapper.Map(sp.GetValue(source), tp.GetValue(target), sp.PropertyType, tp.PropertyType));
        }

        /// <summary>
        /// Custom injections handling to Int32.
        /// </summary>
        internal class Int32 : LoopInjection
        {
            /// <summary>
            /// Enum to Int32
            /// </summary>
            /// <param name="source"></param>
            /// <param name="target"></param>
            /// <returns>true for type changed</returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source.IsSubclassOf(typeof(System.Enum))
                    && target.Equals(typeof(int));
            }
        }
        /// <summary>
        /// Custom injections handling to Enum.
        /// </summary>
        internal class Enum : LoopInjection
        {
            /// <summary>
            /// Int32 to Enum  
            /// </summary>
            /// <param name="source"></param>
            /// <param name="target"></param>
            /// <returns>true for type changed</returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source.Equals(typeof(int))
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
            /// <param name="source"></param>
            /// <param name="target"></param>
            /// <returns></returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return source == Nullable.GetUnderlyingType(target);
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
            /// <param name="source"></param>
            /// <param name="target"></param>
            /// <returns></returns>
            protected override bool MatchTypes(Type source, Type target)
            {
                return Nullable.GetUnderlyingType(source) == target;
            }
        }
    }
}
