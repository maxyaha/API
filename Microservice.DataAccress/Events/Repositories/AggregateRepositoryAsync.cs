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
    public interface IAggregateRepositoryAsync : IRegister
        , IReadOnlyRepository<AggregateRoot>
        , IUpdateRepository<AggregateRoot>
        , ICreateRepository<AggregateRoot, AggregateRoot>
        , IDeleteRepository<AggregateRoot>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class AggregateRepositoryAsync : BaseRepository<EventStoreContext, AggregateRoot, AggregateRoot>, IAggregateRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public AggregateRepositoryAsync(IContext<EventStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
