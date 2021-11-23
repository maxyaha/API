using Shareds.DesignPatterns.CQRS.Events;
using Shareds.Utilizing;

namespace Microservice.Companion.Entities.Events.Maps
{
    /// <summary>
    /// 
    /// </summary>
    public class EventMapper : BaseMapper<DataAccress.Entites.Events.Models.Event, Event>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override DataAccress.Entites.Events.Models.Event ToDomainModel(Event source)
        {
            var target = base.ToDomainModel(source);
            target.Data = BinaryFormatter.Serialize(source);
            return target;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override Event ToDataTransferObject(DataAccress.Entites.Events.Models.Event source)
        {
            var target = BinaryFormatter.Deserialize<Event>(source.Data);
            return target;
        }
    }
}
