using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Events.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        Task<IEnumerable<Event>> GetEvents(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task Save(AggregateRoot aggregate);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        Task<T> GetMemento<T>(Guid id) where T : BaseMemento;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memento"></param>
        /// <returns></returns>
        Task SaveMemento(BaseMemento memento);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Event>> GetAllEvents();
    }
}
