using System.Collections.Generic;

namespace Shareds.DesignPatterns.CQRS.Events.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<IEventHandler<T>> GetHandlers<T>(T type) where T : Event;
    }
}
