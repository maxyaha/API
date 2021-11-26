using Microservice.Companion.Controllers.AggregateRoots.Accounts;
using Microservice.Companion.Controllers.Commands.Accounts;
using Microservice.Companion.Entities.Accounts.Models;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using Shareds.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.CommandHandlers.Accounts
{ /// <summary>
  /// 
  /// </summary>
    public class AccountCommandHandler : ICommandHandler<AccountCommand>
    {
        private readonly IRepository<AccountRoot> repository;
        private readonly ILogger logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public AccountCommandHandler(IRepository<AccountRoot> repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public Command Entry(AccountCommand handle)
        {
            if (handle is null)
                throw new ArgumentNullException(nameof(handle));
            else if (handle.Account is null)
                throw new NotImplementedException();
            else if (this.repository is null)
                throw new InvalidOperationException("Repository is not initialized.");
            else
                logger.Trace(string.Concat(handle.State.ToString(), handle.GetType().Name), handle);

            return handle;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(AccountCommand handle)
        {
            var root = await repository.GetById(Entry(handle).ID).ConfigureAwait(false);

            switch (handle.State)
            {
                case Commandstates.Added:
                    root = new AccountRoot(handle.ID, handle.Account);
                    root.Version = -1;
                    break;
                case Commandstates.Changed when !(root.Account is null) && !Equals(root, handle):
                    root.Change(handle.Account);
                    break;
                case Commandstates.Removed when !(root.Account is null) && Equals(root, handle):
                    root.Remove(handle.Account);
                    break;
                default:
                    var commands = new List<Command>()
                    {
                        new AccountCommand(new AccountDto { ID = handle.Account.ID }),
                        new AccountCommand(handle.Account, handle.Account.ID, 0),
                        new AccountCommand(handle.Account, handle.Account.ID, 1, true)
                    };
                    commands.RemoveAll(o => o.State > handle.State);
                    foreach (var command in commands)
                        if (root.Account is null)
                            await ExecuteAsync(command as AccountCommand).ConfigureAwait(false);
                    break;
            }
            await Task.Run(() => this.repository.Save(root, handle.Version)).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool Equals(AccountRoot source, AccountCommand target)
        {
            return source.Account.Username == target.Account.Username;
        }
    }
}
