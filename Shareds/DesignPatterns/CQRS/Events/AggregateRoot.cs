using Shareds.DesignPatterns.Model;
using Shareds.Utilizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        void LoadsFromHistory(IEnumerable<Event> events);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Event> GetUncommittedChanges();
    }
    /// <summary>
    /// 
    /// </summary>
    public class AggregateRoot : DataTransferObject, IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<Event> events;

        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EventVersion { get; protected set; }


        protected AggregateRoot()
        {
            this.events = new List<Event>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetUncommittedChanges()
        {
            return this.events;
        }
        /// <summary>
        /// 
        /// </summary>
        public void MarkChangesAsCommitted()
        {
            this.events.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        public void LoadsFromHistory(IEnumerable<Event> events)
        {
            switch (events.Count())
            {
                case int count when !count.Equals(0):
                    List<Task> tasks = new List<Task>();

                    foreach (Event @event in events)
                    {
                        ApplyChange(@event, false);
                    }

                    Version = events.Last().Version;
                    break;
                default:
                    break;
            }

            EventVersion = Version;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        /// <param name="isNew"></param>
        private void ApplyChange(Event @event, bool apply)
        {
            dynamic dynamic = this;
            dynamic.Handle(Converter.ChangeTo(@event, @event.GetType()));

            switch (@event)
            {
                case Event entity when apply:
                    this.events.Add(entity);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message) { }
    }
}
