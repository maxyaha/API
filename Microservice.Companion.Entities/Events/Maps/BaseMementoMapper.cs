using Shareds.DesignPatterns.CQRS.Events;
using Shareds.Utilizing;

namespace Microservice.Companion.Entities.Events.Maps
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseMementoMapper : BaseMapper<DataAccress.Entites.Events.Models.BaseMemento, BaseMemento>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override DataAccress.Entites.Events.Models.BaseMemento ToDomainModel(BaseMemento source)
        {
            var target = base.ToDomainModel(source);
            target.Data = BinaryFormatter.Serialize<BaseMemento>(source);
            return target;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override BaseMemento ToDataTransferObject(DataAccress.Entites.Events.Models.BaseMemento source)
        {
            var target = BinaryFormatter.Deserialize<BaseMemento>(source.Data);
            return target;
        }
    }
}
