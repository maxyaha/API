using Microservice.Companion.Controllers.Events.Tester;
using Microservice.Companion.Entities.Tester.Maps;
using Microservice.DataAccress.Tester.Repositories;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.Repository.Extensions;
using Shareds.Logging.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.EventHandlers.Tester
{
    /// <summary>
    /// 
    /// </summary>
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        private readonly ITestRepositoryAsync repository;
        private readonly ILogger logger;
        private readonly TestMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public TestEventHandler(ITestRepositoryAsync repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = new TestMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(TestEvent handle)
        {
            this.logger.Trace(string.Concat(handle.State.ToString(), "TestEvent"), handle);

            var entities = await this.repository.FindByAsync(c => c.ID == handle.AggregateID).ConfigureAwait(false);
            var entity = entities.FirstOrDefault();

            switch (handle.State)
            {
                case Eventstates.Added when entity is null:
                    handle.Test.ID = handle.AggregateID;
                    handle.Test.Version = handle.Version;
                    handle.Test.CreatedDate = handle.Timestamp;
                    handle.Test.ModifiedDate = null;

                    entity = this.mapper.ToDomainModel(handle.Test);

                    await this.repository.Create(entity).ConfigureAwait(false);
                    break;
                case Eventstates.Changed:
                    handle.Test.Version = handle.Version;
                    handle.Test.ModifiedDate = handle.Timestamp;

                    entity = this.mapper.ToDomainModel(handle.Test);

                    await this.repository.Update(entity).ConfigureAwait(false);
                    break;
                case Eventstates.Removed:
                    handle.Test.Version = handle.Version;
                    handle.Test.ModifiedDate = handle.Timestamp;

                    entity = this.mapper.ToDomainModel(handle.Test);

                    await this.repository.Delete(entity).ConfigureAwait(false);
                    break;
                default:
                    break;
            }
        }
    }
}
