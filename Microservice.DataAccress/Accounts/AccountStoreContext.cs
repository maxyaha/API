using Microservice.DataAccress.Entites.Accounts.Models;
using Microservice.DataAccress.Accounts.Configurable;
using Microsoft.EntityFrameworkCore;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using System;

namespace Microservice.DataAccress.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountStoreContext : IDatabaseContextAsync, IDisposable
    {
        DbSet<Account> Account { get; set; }
     
    }
    /// <summary>
    /// 
    /// </summary>
    public class AccountStoreContext : DbContext, IAccountStoreContext
    {
        private readonly bool dispose = false;

        public DbSet<Account> Account { get; set; }


        private void Configure()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static AccountStoreContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public AccountStoreContext() : base()
        {
            Configure();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public AccountStoreContext(DbContextOptions<AccountStoreContext> options) : base(options)
        {
            Configure();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (this.dispose)
                return;

            base.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AccountConfiguration());

       

        }
    }
}
