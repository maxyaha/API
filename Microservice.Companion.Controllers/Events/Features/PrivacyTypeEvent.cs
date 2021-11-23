using Microservice.Companion.Entities.Features.Models;
using Shareds.DesignPatterns.CQRS.Events;
using System;

namespace Microservice.Companion.Controllers.Events.Features
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PrivacyTypeEvent : Event
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public PrivacyTypeEvent(PrivacyTypeDTO entity) : base(Guid.Empty, 0, DateTime.Now, EventState.Added)
        {
            PrivacyType = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public PrivacyTypeEvent(PrivacyTypeDTO entity, Guid aggregateID, int version) : base(aggregateID, version, DateTime.Now, EventState.Changed)
        {
            PrivacyType = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public PrivacyTypeEvent(PrivacyTypeDTO entity, Guid aggregateID, int version, DateTime? timestamp) : base(aggregateID, version, DateTime.Now, EventState.Removed)
        {
            PrivacyType = entity;
        }

        public PrivacyTypeDTO PrivacyType { get; internal set; }
    }
}
