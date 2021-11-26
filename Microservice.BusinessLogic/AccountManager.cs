using Microservice.Companion.Controllers.Commands.Accounts;
using Microservice.Companion.Entities.Accounts.Maps;
using Microservice.Companion.Entities.Accounts.Models;
using Microservice.DataAccress.Accounts.Repositories;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountManager
    {

        Task<IEnumerable<AccountDto>> Accounts();
        Task<AccountDto> Accounts(string username, string password);
        Task<AccountDto> AddAccounts(AccountDto entity);
        Task<AccountDto> UpdateAccounts(Guid id,AccountDto entity);
        Task<AccountDto> DeleteAccounts(Guid id);

    }

    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepositoryAsync repoAccounts;
        private readonly ICommandBus command;


        public AccountManager(IAccountRepositoryAsync repoAccounts
            , ICommandBus command)
        {
            this.repoAccounts = repoAccounts;
            this.command = command;

        }

        public async Task<IEnumerable<AccountDto>> Accounts()
        {
            var queries = await this.repoAccounts.GetAllAsync().ConfigureAwait(false);

            return queries.Select(new AccountMapper().ToDataTransferObject);
        }
        public async Task<AccountDto> Accounts(string username,string password)
        {
            var query = await this.repoAccounts.GetSingleAsync(x => 
                        x.Username.ToLower() == username.ToLower() &&
                        x.Password.Contains(password) &&
                        x.Active
                        ).ConfigureAwait(false);

            return new AccountMapper().ToDataTransferObject(query);
        }

        public async Task<AccountDto> AddAccounts(AccountDto entity)
        {
            Command handle;

            handle = new AccountCommand(entity);
            await this.command.Send(handle as AccountCommand).ConfigureAwait(false);
           
            return entity;
        }

        public async Task<AccountDto> UpdateAccounts(Guid id, AccountDto entity)
        {
            var query = await this.repoAccounts.GetSingleAsync(x => x.ID == id).ConfigureAwait(false);
            var map = new AccountMapper().ToDataTransferObject(query);

            entity.ModifiedDate = map.ModifiedDate;
            entity.CreatedDate = map.CreatedDate;
            entity.Version = map.Version;
            

            Command handle;

            handle = new AccountCommand(entity, id, entity.Version);
            await this.command.Send(handle as AccountCommand).ConfigureAwait(false);

            return entity;
        }

        public async Task<AccountDto> DeleteAccounts(Guid id)
        {
            var query = await this.repoAccounts.GetSingleAsync(x => x.ID == id).ConfigureAwait(false);
            var map = new AccountMapper().ToDataTransferObject(query);

            Command handle;

            handle = new AccountCommand(map, id, map.Version,false);
            await this.command.Send(handle as AccountCommand).ConfigureAwait(false);

            return map;
        }
    }
}
