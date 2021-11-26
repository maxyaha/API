using Microservice.Companion.Entities.Accounts.Models;
using Shareds.DesignPatterns.CQRS.Events;
using System;

namespace Microservice.Companion.Controllers.BaseMementos.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AccountBase : BaseMemento
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public AccountBase(AccountDto entity, Guid id, int version) : base(id, version)
        {
            Account = entity;
        }

        public AccountDto Account { get; set; }
    }
}
