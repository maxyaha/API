using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using Shareds.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventManager manager;

        private static readonly NamedLocker locker =
            // Initialize a readonly locker.
            new NamedLocker();

        public Repository(IEventManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="version"></param>
        public void Save(AggregateRoot aggregate, int version)
        {
            if (!aggregate.GetUncommittedChanges().Any())
                return;

            lock (locker.GetLock(aggregate.ID.ToString()))
            {
                T @object = new T();
                @object = GetById(aggregate.ID).Result;

                switch (@object)
                {
                    case AggregateRoot entity when version == -1:
                        this.manager.Save(aggregate).Wait();
                        break;
                    case AggregateRoot entity when version == entity.Version:
                        this.manager.Save(aggregate).Wait();
                        break;
                    default:
                        throw new ConcurrencyException(string.Format("Aggregate {0} has been previously modified", aggregate.ID));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetById(Guid id)
        {
            var memento = await this.manager.GetMemento<BaseMemento>(id).ConfigureAwait(false);

            IEnumerable<Event> events;
            T @object = new T();

            switch (memento)
            {
                case BaseMemento entity:
                    events = await this.manager.GetEvents(id).ConfigureAwait(false);
                    events = events.Where(o => o.Version >= memento.Version);

                    (@object as IOriginator).SetMemento(memento);
                    break;
                case null:
                    events = await this.manager.GetEvents(id).ConfigureAwait(false);
                    break;
                default:
                    events = null;
                    break;
            }
            await Task.Run(() => @object.LoadsFromHistory(events)).ConfigureAwait(false);

            return @object;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ConcurrencyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ConcurrencyException(string message) : base(message) { }
    }
}
