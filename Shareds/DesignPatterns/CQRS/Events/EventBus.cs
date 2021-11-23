using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class EventBus : IEventBus
    {
        /// <summary>
        /// 
        /// </summary>
        private IEventHandlerFactory @event;

        public EventBus(IEventHandlerFactory @event)
        {
            this.@event = @event;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Publish<T>(T @event) where T : Event
        {
            List<Task> tasks = new List<Task>();

            var handlers = this.@event.GetHandlers<T>();

            foreach (var handler in handlers)
            {
                tasks.Add(handler.Execute(@event));
            }
            await Task.WhenAll(tasks);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task Replay(IEnumerable<Event> events)
        {
            foreach (dynamic @event in events)
            {
                List<Task> tasks = new List<Task>();

                var handlers = this.@event.GetHandlers(@event);

                foreach (var handler in handlers)
                {
                    tasks.Add(handler.Handle(@event));
                }
                await Task.WhenAll(tasks);
            }
        }
    }
}
