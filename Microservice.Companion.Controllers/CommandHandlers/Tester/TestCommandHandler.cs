using Microservice.Companion.Controllers.AggregateRoots.Tester;
using Microservice.Companion.Controllers.Commands.Tester;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using Shareds.Logging.Interfaces;
using System;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.CommandHandlers.Tester
{
    /// <summary>
    /// 
    /// </summary>
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        private readonly IRepository<TestRoot> repository;
        private readonly ILogger logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public TestCommandHandler(IRepository<TestRoot> repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task Execute(TestCommand handle)
        {
            if (handle == null)
                throw new ArgumentNullException("command");
            if (this.repository == null)
                throw new InvalidOperationException("Repository is not initialized.");
            if (handle.Test is null)
                return;

            this.logger.Trace(string.Concat(handle.State.ToString(), "TestCommand"), handle);

            var aggregate = await this.repository.GetById(handle.ID).ConfigureAwait(false);

            switch (handle.State)
            {
                case CommandState.Added when aggregate.Test is null:
                    aggregate = new TestRoot(handle.ID, handle.Test);
                    aggregate.Version = -1;
                    break;
                case CommandState.Changed:
                    if (aggregate.Test.Name != handle.Test.Name)
                        goto change;
                    else break;
                    change: aggregate.Change(handle.Test);
                    break;
                case CommandState.Removed:
                    aggregate.Remove(handle.Test);
                    break;
                default:
                    break;
            }
            await Task.Run(() => this.repository.Save(aggregate, handle.Version)).ConfigureAwait(false);
        }
    }

}
