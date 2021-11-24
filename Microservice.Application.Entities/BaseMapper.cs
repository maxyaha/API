using Shareds.DesignPatterns.Model;
using Shareds.Mapping;

namespace Microservice.Application.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataTransferObject"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseMapper<TDataTransferObject, TEntity>
        where TDataTransferObject : DataTransferObject
        where TEntity : PresentModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDataTransferObject ToDataTransferObject(TEntity source)
        {
            return source is null
                ? null
                : Mapper.Map<TEntity, TDataTransferObject>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TEntity ToPresentationModel(TDataTransferObject source)
        {
            return source is null
                ? null
                : Mapper.Map<TDataTransferObject, TEntity>(source);
        }
    }

    public abstract class BaseMapperWrapper<TDataTransferObject, TEntity>
      where TDataTransferObject : DataTransferObjectWrapper
      where TEntity : PresentModelWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDataTransferObject ToDataTransferObject(TEntity source)
        {
            return source is null
                ? null
                : Mapper.Map<TEntity, TDataTransferObject>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TEntity ToPresentationModel(TDataTransferObject source)
        {
            return source is null
                ? null
                : Mapper.Map<TDataTransferObject, TEntity>(source);
        }
    }
}
