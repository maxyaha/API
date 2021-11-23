using Microservice.DataAccress.Entites.Events.Models;
using Shareds.DesignPatterns.IoC.Interfaces;
using Shareds.DesignPatterns.Repository;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.DesignPatterns.Repository.Interfaces;
using Shareds.Logging.Interfaces;

namespace Microservice.DataAccress.Events.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventRepositoryAsync : IRegister
        , IReadOnlyRepository<Event>
        , IUpdateRepository<Event>
        , ICreateRepository<Event, Event>
        , IDeleteRepository<Event>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class EventRepositoryAsync : BaseRepository<EventStoreContext, Event, Event>, IEventRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public EventRepositoryAsync(IContext<EventStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
