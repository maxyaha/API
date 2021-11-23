using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.IoC;
using System.Collections.Generic;

namespace Shareds.DesignPatterns.CQRS.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class EventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            return DependencyInjection.Container.GetAllInstances<IEventHandler<T>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<IEventHandler<T>> GetHandlers<T>(T type) where T : Event
        {
            return DependencyInjection.Container.GetAllInstances<IEventHandler<T>>();
        }
    }
}
