using Microservice.Companion.Entities.Events.Maps;
using Microservice.DataAccress.Events.Repositories;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.Repository.Extensions;
using Shareds.Utilizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.BusinessLogic
{
    /// <summary>
    /// /
    /// </summary>
    public class EventManager : IEventManager
    {
        private readonly IEventBus @event;
        private readonly IAggregateRepositoryAsync repoAggregateRoot;
        private readonly IBaseMementoRepositoryAsync repoBaseMemento;
        private readonly IEventRepositoryAsync repoEvent;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoEvent"></param>
        /// <param name="repoAggregateRoot"></param>
        /// <param name="repoBaseMemento"></param>
        /// <param name="event"></param>
        public EventManager(IEventRepositoryAsync repoEvent, IAggregateRepositoryAsync repoAggregateRoot, IBaseMementoRepositoryAsync repoBaseMemento, IEventBus @event)
        {
            this.@event = @event;

            this.repoAggregateRoot = repoAggregateRoot;
            this.repoBaseMemento = repoBaseMemento;
            this.repoEvent = repoEvent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var events = await this.repoEvent.GetAllAsync()
                // Await, Get all data from database.
                .ConfigureAwait(false);

            return events.Select(new EventMapper().ToDataTransferObject);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Event>> GetEvents(Guid aggregateID)
        {
            var events = await this.repoEvent.FindByAsync(o => o.AggregateID == aggregateID)
                // Await, Get data from database by ID.
                .ConfigureAwait(false);

            return events.Select(new EventMapper().ToDataTransferObject);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aggregateID"></param>
        /// <returns></returns>
        public async Task<T> GetMemento<T>(Guid aggregateID) where T : BaseMemento
        {
            var mementos = await this.repoBaseMemento.FindByAsync(o => o.ID == aggregateID)
                // Await, Get data from database by ID.
                .ConfigureAwait(false);

            var memento = mementos.LastOrDefault();

            return memento is null
                ? null
                : new BaseMementoMapper().ToDataTransferObject(memento) as T;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public async Task Save(AggregateRoot aggregate)
        {
            var events = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            foreach (var @event in events)
            {
                version++;
                aggregate.Version = version;
                @event.Version = version;

                var entities = await this.repoAggregateRoot.FindByAsync(x => x.ID == aggregate.ID)
                    // Await, Get data from database by ID.
                    .ConfigureAwait(false);

                var entity = entities.FirstOrDefault();

                switch (@event.State)
                {
                    case EventState.Added when version == 0 && entity == null:
                        entity = await this.repoAggregateRoot.Create(new AggregateRootMapper().ToDomainModel(aggregate))
                            // Await, Post data to database.
                            .ConfigureAwait(false);

                        @event.AggregateID = entity.ID;

                        await this.repoEvent.Create(new EventMapper().ToDomainModel(@event))
                            // Await, Post data to database.
                            .ConfigureAwait(false);
                        break;
                    case EventState.Changed when version >= 1 && entity != null:
                        entity.Version = version;

                        await this.repoAggregateRoot.Update(entity)
                            // Await, Put data to database.
                            .ConfigureAwait(false);
                        await this.repoEvent.Create(new EventMapper().ToDomainModel(@event))
                            // Await, Post data to database.
                            .ConfigureAwait(false);
                        break;
                    case EventState.Removed:
                        break;
                    default:
                        throw new NullReferenceException();
                }
                // Convert to event handlers.
                var eventer = Converter.ChangeTo(@event, @event.GetType());

                await this.@event.Publish(eventer)
                    // Await, Manage tasks according to event handlers.
                    .ConfigureAwait(false);

                aggregate.ID = @event.AggregateID;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memento"></param>
        /// <returns></returns>
        public async Task SaveMemento(BaseMemento memento)
        {
            await this.repoBaseMemento.Create(new BaseMementoMapper().ToDomainModel(memento))
                // Await, Post data to database.
                .ConfigureAwait(false);
        }
    }
}
