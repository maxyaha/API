using Microservice.DataAccress.Entites.Tester.Models;
using Shareds.DesignPatterns.IoC.Interfaces;
using Shareds.DesignPatterns.Repository;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.DesignPatterns.Repository.Interfaces;
using Shareds.Logging.Interfaces;

namespace Microservice.DataAccress.Tester.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITestRepositoryAsync : IRegister
        , IReadOnlyRepository<Test>
        , IUpdateRepository<Test>
        , ICreateRepository<Test, Test>
        , IDeleteRepository<Test>
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class TestRepositoryAsync : BaseRepository<TesterStoreContext, Test, Test>, ITestRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public TestRepositoryAsync(IContext<TesterStoreContext> context, ILogger logger) : base(context, logger)
        {
        }
    }
}
