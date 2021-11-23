using Microservice.DataAccress.Entites.Features.Models;
using Shareds.DesignPatterns.IoC.Interfaces;
using Shareds.DesignPatterns.Repository;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.DesignPatterns.Repository.Interfaces;
using Shareds.Logging.Interfaces;

namespace Microservice.DataAccress.Features.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface INewsRepositoryAsync : IRegister
        , IReadOnlyRepository<News>
        , IUpdateRepository<News>
        , ICreateRepository<News, News>
        , IDeleteRepository<News>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class NewsRepositoryAsync : BaseRepository<FeatureStoreContext, News, News>, INewsRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public NewsRepositoryAsync(IContext<FeatureStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
