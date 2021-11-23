using System.Threading.Tasks;

namespace Shareds.DesignPatterns.Repository.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDeleteRepository<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(TKey id);
    }
}
