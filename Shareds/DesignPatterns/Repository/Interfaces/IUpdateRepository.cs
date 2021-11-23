using Shareds.DesignPatterns.Model;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.Repository.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public interface IUpdateRepository<TInput> where TInput : DomainModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(TInput entity);
    }
}
