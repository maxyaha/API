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
    public interface IFeatureRepositoryAsync : IRegister
        , IReadOnlyRepository<Feature>
        , IUpdateRepository<Feature>
        , ICreateRepository<Feature, Feature>
        , IDeleteRepository<Feature>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class FeatureRepositoryAsync : BaseRepository<FeatureStoreContext, Feature, Feature>, IFeatureRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public FeatureRepositoryAsync(IContext<FeatureStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
