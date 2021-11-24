using Newtonsoft.Json.Linq;
using Omu.ValueInjecter;
using Shareds.Mapping.Extensions;
using Shareds.Mapping.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Shareds.Mapping
{
    /// <summary>
    /// Borrowed from http://valueinjecter.codeplex.com/SourceControl/changeset/view/76924#1204894
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly IDictionary<Type, object> mappers = new Dictionary<Type, object>();

        /// <summary>
        /// Map source to an existing target.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(TSource source, TTarget target)
        {
            return Get<TSource, TTarget>()
                .Map(source, target);
        }
        /// <summary>
        /// Create a new target and map source on it.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            TTarget target = (TTarget)typeof(TTarget).Create();

            return Get<TSource, TTarget>()
                .Map(source, target);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object Map(object source, object target, Type sourceType, Type targetType)
        {
            target = target
                ?? targetType.Create();

            MethodInfo method = typeof(Mapper)
                .GetMethod("Get")
                .MakeGenericMethod(sourceType, targetType);

            object entity = method.Invoke(null, null);

            method = entity
                .GetType()
                .GetMethod("Map");

            return method.Invoke(entity, new[] { source, target });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        public static IMapper<TSource, TTarget> Get<TSource, TTarget>()
        {
            IMapper<TSource, TTarget> mapper = new Mapper<TSource, TTarget>();

            if (mappers.ContainsKey(typeof(IMapper<TSource, TTarget>)))
                mapper = mappers[typeof(IMapper<TSource, TTarget>)] as IMapper<TSource, TTarget>;
            else if (typeof(TSource).IsEnumerable() && typeof(TTarget).IsEnumerable())
                mapper = Activator.CreateInstance(typeof(Mappers<,>)
                    .MakeGenericType(typeof(TSource), typeof(TTarget))) as IMapper<TSource, TTarget>;

            return mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="entity"></param>
        public static void Add<TSource, TTarget>(IMapper<TSource, TTarget> entity)
        {
            mappers.Add(typeof(IMapper<TSource, TTarget>), entity);
        }
        /// <summary>
        /// 
        /// </summary>
        public static void Clear()
        {
            mappers.Clear();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public class Mapper<TSource, TTarget> : IMapper<TSource, TTarget>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual TTarget Map(TSource sources, TTarget targets)
        {
            if (sources is JObject)
                targets.InjectFrom(sources);

            targets.InjectFrom(sources)
                .InjectFrom<ConverterExtensions.Normal>(sources)
                .InjectFrom<ConverterExtensions.Nullables>(sources)
                .InjectFrom<ConverterExtensions.Enum>(sources)
                .InjectFrom<ConverterExtensions.Int32>(sources)
                .InjectFrom<ConverterExtensions>(sources);

            return targets;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public class Mappers<TSource, TTarget> : IMapper<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public TTarget Map(TSource sources, TTarget targets)
        {
            if (sources is null)
                // Return null, If TSource is null.
                return null;

            Type type = typeof(TTarget)
                .GetGenericArguments()[0];
            Type types = typeof(List<>)
                .MakeGenericType(type);

            object entity = Activator.CreateInstance(types);

            MethodInfo method = entity
                .GetType()
                .GetMethod("Add");

            foreach (object source in sources as IEnumerable)
            {
                var target = type.Create();

                var parameters = new[] { Mapper.Map(source, target, source.GetType(), target.GetType()) };

                method.Invoke(entity, parameters);
            }
            return entity as TTarget;
        }
    }
}
