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
    public interface IPrivacyTypeRepositoryAsync : IRegister
        , IReadOnlyRepository<PrivacyType>
        , IUpdateRepository<PrivacyType>
        , ICreateRepository<PrivacyType, PrivacyType>
        , IDeleteRepository<PrivacyType>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyTypeRepositoryAsync : BaseRepository<FeatureStoreContext, PrivacyType, PrivacyType>, IPrivacyTypeRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public PrivacyTypeRepositoryAsync(IContext<FeatureStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }

}
