using Microservice.Companion.Controllers.AggregateRoots.Features;
using Microservice.Companion.Controllers.Commands.Features;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using Shareds.Logging.Interfaces;
using System;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.CommandHandlers.Features
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyTypeCommandHandler : ICommandHandler<PrivacyTypeCommand>
    {
        private readonly IRepository<PrivacyTypeRoot> repository;
        private readonly ILogger logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public PrivacyTypeCommandHandler(IRepository<PrivacyTypeRoot> repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task Execute(PrivacyTypeCommand handle)
        {
            if (handle == null)
                throw new ArgumentNullException("command");
            if (this.repository == null)
                throw new InvalidOperationException("Repository is not initialized.");
            if (handle.PrivacyType is null)
                return;

            this.logger.Trace(string.Concat(handle.State.ToString(), "PrivacyTypeCommand"), handle);

            var aggregate = await this.repository.GetById(handle.ID).ConfigureAwait(false);

            switch (handle.State)
            {
                case CommandState.Added when aggregate.PrivacyType is null:
                    aggregate = new PrivacyTypeRoot(handle.ID, handle.PrivacyType);
                    aggregate.Version = -1;
                    break;
                case CommandState.Changed:
                    if (aggregate.PrivacyType.Name != handle.PrivacyType.Name)
                        goto change;
                    else break;
                    change: aggregate.Change(handle.PrivacyType);
                    break;
                case CommandState.Removed:
                    aggregate.Remove(handle.PrivacyType);
                    break;
                default:
                    break;
            }
            await Task.Run(() => this.repository.Save(aggregate, handle.Version)).ConfigureAwait(false);
        }
    }

}
