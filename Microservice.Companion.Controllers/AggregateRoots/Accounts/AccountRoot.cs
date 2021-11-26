using Microservice.Companion.Controllers.BaseMementos.Accounts;
using Microservice.Companion.Controllers.Events.Accounts;
using Microservice.Companion.Entities.Accounts.Models;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using System;

namespace Microservice.Companion.Controllers.AggregateRoots.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountRoot : AggregateRoot, IHandle<AccountEvent>, IOriginator
    {
        public AccountRoot()
        {
        }

        public AccountDto Account { get; private set; }

        #region Internal command implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        internal AccountRoot(Guid id, AccountDto entity)
        {
            ApplyChange(new AccountEvent(entity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        internal void Change(AccountDto entity)
        {
            ApplyChange(new AccountEvent(entity, ID, Version));
        }
        /// <summary>
        /// 
        /// </summary>
        internal void Remove(AccountDto entity)
        {
            ApplyChange(new AccountEvent(entity, ID, Version, true));
        }

        #endregion Internal command implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public void Handle(AccountEvent handle)
        {
            ID = handle.AggregateID;
            Version = handle.Version;
            Account = handle.Account;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memento"></param>
        public void SetMemento(BaseMemento memento)
        {
            ID = memento.ID;
            Version = memento.Version;
            Account = ((AccountBase)memento).Account;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseMemento GetMemento()
        {
            return new AccountBase(Account, ID, Version);
        }
    }
}
