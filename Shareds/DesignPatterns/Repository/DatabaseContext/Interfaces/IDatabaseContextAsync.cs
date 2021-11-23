using System.Threading;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseContextAsync : IDatabaseContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
