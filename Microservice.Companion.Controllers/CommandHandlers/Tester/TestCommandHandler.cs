using Microservice.Companion.Controllers.AggregateRoots.Tester;
using Microservice.Companion.Controllers.Commands.Tester;
using Microservice.Companion.Entities.Tester.Models;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using Shareds.Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.CommandHandlers.Tester
{ /// <summary>
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
        public Command Entry(TestCommand handle)
        {
            if (handle is null)
                throw new ArgumentNullException(nameof(handle));
            else if (handle.Test is null)
                throw new NotImplementedException();
            else if (this.repository is null)
                throw new InvalidOperationException("Repository is not initialized.");
            else
                logger.Trace(string.Concat(handle.State.ToString(), handle.GetType().Name), handle);

            return handle;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(TestCommand handle)
        {
            var root = await repository.GetById(Entry(handle).ID).ConfigureAwait(false);

            switch (handle.State)
            {
                case Commandstates.Added:
                    root = new TestRoot(handle.ID, handle.Test);
                    root.Version = -1;
                    break;
                case Commandstates.Changed when !(root.Test is null) && !Equals(root, handle):
                    root.Change(handle.Test);
                    break;
                case Commandstates.Removed when !(root.Test is null) && Equals(root, handle):
                    root.Remove(handle.Test);
                    break;
                default:
                    var commands = new List<Command>()
                    {
                        new TestCommand(new TestDto { ID = handle.Test.ID }),
                        new TestCommand(handle.Test, handle.Test.ID, 0),
                        new TestCommand(handle.Test, handle.Test.ID, 1, true)
                    };
                    commands.RemoveAll(o => o.State > handle.State);
                    foreach (var command in commands)
                        if (root.Test is null)
                            await ExecuteAsync(command as TestCommand).ConfigureAwait(false);
                    break;
            }
            await Task.Run(() => this.repository.Save(root, handle.Version)).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool Equals(TestRoot source, TestCommand target)
        {
            return source.Test.Name == target.Test.Name;
        }
    }
}
