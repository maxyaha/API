using Shareds.DesignPatterns.Model;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.Repository.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICreateRepository<TInput, TKey> where TInput : DomainModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TKey> Create(TInput entity);
    }
}
