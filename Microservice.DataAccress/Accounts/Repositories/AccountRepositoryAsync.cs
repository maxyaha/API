using Microservice.DataAccress.Entites.Accounts.Models;
using Shareds.DesignPatterns.IoC.Interfaces;
using Shareds.DesignPatterns.Repository;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.DesignPatterns.Repository.Interfaces;
using Shareds.Logging.Interfaces;

namespace Microservice.DataAccress.Accounts.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountRepositoryAsync : IRegister
        , IReadOnlyRepository<Account>
        , IUpdateRepository<Account>
        , ICreateRepository<Account, Account>
        , IDeleteRepository<Account>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class AccountRepositoryAsync : BaseRepository<AccountStoreContext, Account, Account>, IAccountRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public AccountRepositoryAsync(IContext<AccountStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
