using Microservice.Companion.Entities.Accounts.Models;
using Shareds.DesignPatterns.CQRS.Events;
using System;

namespace Microservice.Companion.Controllers.Events.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AccountEvent : Event
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public AccountEvent(AccountDto entity) : base(Guid.Empty, 0, DateTime.Now, Eventstates.Added)
        {
            Account = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public AccountEvent(AccountDto entity, Guid aggregateID, int version) : base(aggregateID, version, DateTime.Now, Eventstates.Changed)
        {
            Account = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public AccountEvent(AccountDto entity, Guid aggregateID, int version, bool active) : base(aggregateID, version, DateTime.Now, Eventstates.Removed)
        {
            Account = entity;
        }

        public AccountDto Account { get; internal set; }
    }
}
