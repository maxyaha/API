using Microservice.Companion.Controllers.Events.Features;
using Microservice.Companion.Entities.Features.Maps;
using Microservice.DataAccress.Features.Repositories;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.Repository.Extensions;
using Shareds.Logging.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers.EventHandlers.Features
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyTypeEventHandler : IEventHandler<PrivacyTypeEvent>
    {
        private readonly IPrivacyTypeRepositoryAsync repository;
        private readonly ILogger logger;
        private readonly PrivacyTypeMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public PrivacyTypeEventHandler(IPrivacyTypeRepositoryAsync repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = new PrivacyTypeMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public async Task Execute(PrivacyTypeEvent handle)
        {
            this.logger.Trace(string.Concat(handle.State.ToString(), "PrivacyTypeEvent"), handle);

            var entities = await this.repository.FindByAsync(c => c.ID == handle.AggregateID).ConfigureAwait(false);
            var entity = entities.FirstOrDefault();

            switch (handle.State)
            {
                case EventState.Added when entity is null:
                    handle.PrivacyType.ID = handle.AggregateID;
                    handle.PrivacyType.Version = handle.Version;
                    handle.PrivacyType.CreatedDate = handle.Timestamp;
                    handle.PrivacyType.ModifiedDate = null;

                    entity = this.mapper.ToDomainModel(handle.PrivacyType);

                    await this.repository.Create(entity).ConfigureAwait(false);
                    break;
                case EventState.Changed:
                    handle.PrivacyType.Version = handle.Version;
                    handle.PrivacyType.ModifiedDate = handle.Timestamp;

                    entity = this.mapper.ToDomainModel(handle.PrivacyType);

                    await this.repository.Update(entity).ConfigureAwait(false);
                    break;
                case EventState.Removed:
                    handle.PrivacyType.Version = handle.Version;
                    handle.PrivacyType.ModifiedDate = handle.Timestamp;

                    entity = this.mapper.ToDomainModel(handle.PrivacyType);

                    await this.repository.Delete(entity).ConfigureAwait(false);
                    break;
                default:
                    break;
            }
        }
    }
}
