using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using System;

namespace Shareds.DesignPatterns.CQRS.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandBus"></typeparam>
    public abstract class ObjectInitializer<TCommandBus> : IObjectInitializer<TCommandBus> where TCommandBus : CommandBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected abstract void SeedAsync(TCommandBus command);

        /// <summary>
        /// 
        /// </summary>
        public ObjectInitializer()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public void InitializeObject(TCommandBus command)
        {
            try
            {
                SeedAsync(command);
            }
            catch (Exception)
            {
            }
        }
    }
}
