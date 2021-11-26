using Microservice.Companion.Entities.Accounts.Models;
using Shareds.DesignPatterns.CQRS.Commands;
using System;

namespace Microservice.Companion.Controllers.Commands.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountCommand : Command
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public AccountCommand(AccountDto entity) : base(Guid.Empty, -1, Commandstates.Added)
        {
            Account = entity;
            Account.Active = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public AccountCommand(AccountDto entity, Guid id, int version) : base(id, version, Commandstates.Changed)
        {
            Account = entity;
            Account.Active = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public AccountCommand(AccountDto entity, Guid id, int version, bool active) : base(id, version, Commandstates.Removed)
        {
            Account = entity;
            Account.Active = active;
        }

        public AccountDto Account { get; private set; }
    }
}
