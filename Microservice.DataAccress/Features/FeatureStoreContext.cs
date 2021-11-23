using Microservice.DataAccress.Entites.Features.Models;
using Microservice.DataAccress.Features.Configurable;
using Microsoft.EntityFrameworkCore;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using System;

namespace Microservice.DataAccress.Features
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFeatureStoreContext : IDatabaseContextAsync, IDisposable
    {
        DbSet<Feature> Features { get; set; }
        DbSet<Guide> Guides { get; set; }
        DbSet<News> News { get; set; }
        DbSet<PrivacyType> PrivacyTypes { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class FeatureStoreContext : DbContext, IFeatureStoreContext
    {
        private readonly bool dispose = false;

        public DbSet<Feature> Features { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PrivacyType> PrivacyTypes { get; set; }

        private void Configure()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static FeatureStoreContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public FeatureStoreContext() : base()
        {
            Configure();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public FeatureStoreContext(DbContextOptions<FeatureStoreContext> options) : base(options)
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

            builder.ApplyConfiguration(new FeatureConfiguration());
            builder.ApplyConfiguration(new GuideConfiguration());
            builder.ApplyConfiguration(new NewsConfiguration());
            builder.ApplyConfiguration(new PrivacyTypeConfiguration());

       

        }
    }
}
