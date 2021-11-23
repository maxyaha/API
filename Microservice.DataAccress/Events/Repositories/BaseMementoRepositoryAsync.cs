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
    public interface IBaseMementoRepositoryAsync : IRegister
        , IReadOnlyRepository<BaseMemento>
        , IUpdateRepository<BaseMemento>
        , ICreateRepository<BaseMemento, BaseMemento>
        , IDeleteRepository<BaseMemento>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class BaseMementoRepositoryAsync : BaseRepository<EventStoreContext, BaseMemento, BaseMemento>, IBaseMementoRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public BaseMementoRepositoryAsync(IContext<EventStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
