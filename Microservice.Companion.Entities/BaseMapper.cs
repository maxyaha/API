using Shareds.DesignPatterns.Model;
using Shareds.Mapping;

namespace Microservice.Companion.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainModel"></typeparam>
    /// <typeparam name="TDataTransferObject"></typeparam>
    public abstract class BaseMapper<TDomainModel, TDataTransferObject>
        where TDomainModel : DomainModel
        where TDataTransferObject : DataTransferObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDomainModel ToDomainModel(TDataTransferObject source)
        {
            return source == null
                ? null
                : Mapper.Map<TDataTransferObject, TDomainModel>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public virtual TDataTransferObject ToDataTransferObject(TDomainModel source)
        {
            return source == null
                ? null
                : Mapper.Map<TDomainModel, TDataTransferObject>(source);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDataTransferObject"></typeparam>
    public class BaseMapper<TDataTransferObject>
        where TDataTransferObject : BaseModeler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void Copy(TDataTransferObject source, ref TDataTransferObject target)
        {
            var @base = target
                as BaseModeler;

            target = source is null
                ? null
                : Mapper.Map<TDataTransferObject, TDataTransferObject>(source);

            target.ID = @base.ID;
            target.CreatedDate = @base.CreatedDate;
            target.ModifiedDate = @base.ModifiedDate;
            target.Version = @base.Version;
        }
    }
}
