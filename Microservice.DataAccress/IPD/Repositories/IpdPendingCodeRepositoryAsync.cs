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
    public interface IIpdPendingCodeRepositoryAsync : IRegister
        , IReadOnlyRepository<IpdPendingCode>
        , IUpdateRepository<IpdPendingCode>
        , ICreateRepository<IpdPendingCode, IpdPendingCode>
        , IDeleteRepository<IpdPendingCode>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class IpdPendingCodeRepositoryAsync : BaseRepository<IpdStoreContext, IpdPendingCode, IpdPendingCode>, IIpdPendingCodeRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public IpdPendingCodeRepositoryAsync(IContext<IpdStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
