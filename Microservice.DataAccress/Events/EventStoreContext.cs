using Microservice.DataAccress.Entites.Events.Models;
using Microservice.DataAccress.Events.Configurable;
using Microsoft.EntityFrameworkCore;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using System;

namespace Microservice.DataAccress.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventStoreContext : IDatabaseContextAsync, IDisposable
    {
        DbSet<AggregateRoot> AggregateRoots { get; set; }
        DbSet<BaseMemento> BaseMementoes { get; set; }
        DbSet<Event> Events { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class EventStoreContext : DbContext, IEventStoreContext
    {
        /// <summary>
        /// Avoid dispose for testing.
        /// </summary>
        private readonly bool dispose = false;

        /// <summary>
        /// 
        /// </summary>
        public DbSet<AggregateRoot> AggregateRoots { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<BaseMemento> BaseMementoes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private void Configure()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static EventStoreContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public EventStoreContext() : base()
        {
            Configure();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options)
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

            builder.ApplyConfiguration(new AggregateRootConfiguration());
            builder.ApplyConfiguration(new BaseMementoConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
        }
    }
}
