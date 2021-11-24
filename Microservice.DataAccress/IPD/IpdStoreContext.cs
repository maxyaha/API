using Microsoft.EntityFrameworkCore;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Microservice.DataAccress.Entites.IPD.Models;
using Microservice.DataAccress.IPD.Configurable;

namespace Microservice.DataAccress.IPD
{
    /// <summary>
    /// Database set for context of party-store
    /// </summary>
    public interface IIpdStoreContext : IDatabaseContextAsync
    {

        DbSet<PendingCode> PendingCode { get; set; }
        DbSet<IpdPendingCode> IpdPendingCode { get; set; }
        
    }
    /// <summary>
    /// Database context of documentnumber-store
    /// </summary>
    public class IpdStoreContext : DbContext, IIpdStoreContext
    {
        private readonly bool dispose = false;

        public DbSet<PendingCode> PendingCode { get; set; }
        public DbSet<IpdPendingCode> IpdPendingCode { get; set; }
 

        /// <summary>
        /// 
        /// </summary>
        static IpdStoreContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public IpdStoreContext() : base()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public IpdStoreContext(DbContextOptions<IpdStoreContext> options) : base(options)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            if (this.dispose)
                return;

            base.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PendingCodeConfiguration());
            modelBuilder.ApplyConfiguration(new IpdPendingCodeConfiguration());
        }
    }
}
