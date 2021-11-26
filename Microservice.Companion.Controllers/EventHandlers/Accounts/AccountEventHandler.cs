using Microservice.Companion.Controllers.Events.Accounts;
using Microservice.Companion.Entities.Accounts.Maps;
using Microservice.DataAccress.Accounts.Repositories;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.Repository.Extensions;
using Shareds.Logging.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.EventHandlers.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountEventHandler : IEventHandler<AccountEvent>
    {
        private readonly IAccountRepositoryAsync repository;
        private readonly ILogger logger;
        private readonly AccountMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public AccountEventHandler(IAccountRepositoryAsync repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = new AccountMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(AccountEvent handle)
        {
            this.logger.Trace(string.Concat(handle.State.ToString(), "AccountEvent"), handle);

            var entities = await this.repository.FindByAsync(c => c.ID == handle.AggregateID).ConfigureAwait(false);
            var entity = entities.FirstOrDefault();

            switch (handle.State)
            {
                case Eventstates.Added when entity is null:
                    handle.Account.ID = handle.AggregateID;
                    handle.Account.Version = handle.Version;
                    handle.Account.CreatedDate = handle.Timestamp;
                    handle.Account.ModifiedDate = null;

                    entity = this.mapper.ToDomainModel(handle.Account);

                    await this.repository.Create(entity).ConfigureAwait(false);
                    break;
                case Eventstates.Changed:
                    handle.Account.Version = handle.Version;
                    handle.Account.ModifiedDate = handle.Timestamp;

                    entity = this.mapper.ToDomainModel(handle.Account);

                    await this.repository.Update(entity).ConfigureAwait(false);
                    break;
                case Eventstates.Removed:
                    handle.Account.Version = handle.Version;
                    handle.Account.ModifiedDate = handle.Timestamp;

                    entity = this.mapper.ToDomainModel(handle.Account);

                    await this.repository.Delete(entity).ConfigureAwait(false);
                    break;
                default:
                    break;
            }
        }
    }
}
