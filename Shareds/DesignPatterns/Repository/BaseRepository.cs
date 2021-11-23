using Microsoft.EntityFrameworkCore;
using Shareds.DesignPatterns.Model;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.DesignPatterns.Repository.Interfaces;
using Shareds.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.Repository
{
    /// <summary>
    /// Represents a repository of items.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type">type</see> of item in the repository.</typeparam>
    public abstract class BaseRepository<TContext, TEntity, TKey> : IReadOnlyRepository<TEntity>
        where TContext : IDatabaseContextAsync
        where TEntity : DomainModel
        where TKey : DomainModel
    {
        private ILogger logger;
        private readonly IContext<TContext> context;

        protected BaseRepository(IContext<TContext> context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, CancellationToken token)
        {
            TContext context = this.context.DatabaseContext;

            return await query(context.Set<TEntity>()).ToListAsync(token).ConfigureAwait(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, TResult> query, CancellationToken token)
        {
            TaskFactory<TResult> task = Task<TResult>.Factory;

            TContext context = this.context.DatabaseContext;

            return await task.StartNew(() => query( this.context.DatabaseContext.Set<TEntity>()), token).ConfigureAwait(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TKey> Create(TEntity entity)
        {
            TContext context = this.context.DatabaseContext;

            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Added;

            DbSet<TEntity> entities = context.Set<TEntity>();
            entities.Add(entity);

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException ex)
            {
                this.logger.LogException(ex);
                throw;
            }
            catch (ValidationException ex)
            {
                StringBuilder sb = new StringBuilder(ex?.Message);
                //sb.Append(ex?.Message);

                this.logger.Fatal(sb.ToString());
                this.logger.LogException(ex);
                throw;
            }
            return entity as TKey;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Delete(TKey key)
        {
            TContext context = this.context.DatabaseContext;

            context.Entry(key).State = EntityState.Deleted;

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                this.logger.LogException(ex);
                throw new Exception("EF Validation failed, see inner exception for details", ex);
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            TContext context = this.context.DatabaseContext;

            context.Entry(entity).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                this.logger.LogException(ex);

                context.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
            return true;
        }
    }
}
