using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Events.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Publish<T>(T @event) where T : Event;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        Task Replay(IEnumerable<Event> events);
    }
}
