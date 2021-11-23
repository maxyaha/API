using Shareds.DesignPatterns.CQRS.Events;
using System;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Repositories.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="version"></param>
        void Save(AggregateRoot aggregate, int version);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(Guid id);
    }
}
