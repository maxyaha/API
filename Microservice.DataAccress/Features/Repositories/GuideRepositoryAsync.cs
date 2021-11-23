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
    public interface IGuideRepositoryAsync : IRegister
        , IReadOnlyRepository<Guide>
        , IUpdateRepository<Guide>
        , ICreateRepository<Guide, Guide>
        , IDeleteRepository<Guide>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class GuideRepositoryAsync : BaseRepository<FeatureStoreContext, Guide, Guide>, IGuideRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public GuideRepositoryAsync(IContext<FeatureStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
