using Microservice.DataAccress.Entites.IPD.Models;
using Shareds.DesignPatterns.IoC.Interfaces;
using Shareds.DesignPatterns.Repository;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.DesignPatterns.Repository.Interfaces;
using Shareds.Logging.Interfaces;

namespace Microservice.DataAccress.IPD.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPendingCodeRepositoryAsync : IRegister
        , IReadOnlyRepository<PendingCode>
        , IUpdateRepository<PendingCode>
        , ICreateRepository<PendingCode, PendingCode>
        , IDeleteRepository<PendingCode>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class PendingCodeRepositoryAsync : BaseRepository<IpdStoreContext, PendingCode, PendingCode>, IPendingCodeRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public PendingCodeRepositoryAsync(IContext<IpdStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
