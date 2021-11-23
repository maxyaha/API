using Microservice.DataAccress.Entites.Tester.Models;
using Microservice.DataAccress.Tester.Configurable;
using Microsoft.EntityFrameworkCore;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using System;

namespace Microservice.DataAccress.Tester
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITesterStoreContext : IDatabaseContextAsync, IDisposable
    {
        DbSet<Test> Test { get; set; }
     
    }
    /// <summary>
    /// 
    /// </summary>
    public class TesterStoreContext : DbContext, ITesterStoreContext
    {
        private readonly bool dispose = false;

        public DbSet<Test> Test { get; set; }


        private void Configure()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static TesterStoreContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public TesterStoreContext() : base()
        {
            Configure();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public TesterStoreContext(DbContextOptions<TesterStoreContext> options) : base(options)
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

            builder.ApplyConfiguration(new TestConfiguration());
    

       

        }
    }
}
