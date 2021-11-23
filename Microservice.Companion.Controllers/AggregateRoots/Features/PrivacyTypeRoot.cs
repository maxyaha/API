using Microservice.Companion.Controllers.BaseMementos.Features;
using Microservice.Companion.Controllers.Events.Features;
using Microservice.Companion.Entities.Features.Models;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using System;

namespace Microservice.Companion.Controllers.AggregateRoots.Features
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyTypeRoot : AggregateRoot, IHandle<PrivacyTypeEvent>, IOriginator
    {
        public PrivacyTypeRoot()
        {
        }

        public PrivacyTypeDTO PrivacyType { get; private set; }

        #region Internal command implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        internal PrivacyTypeRoot(Guid id, PrivacyTypeDTO entity)
        {
            ApplyChange(new PrivacyTypeEvent(entity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        internal void Change(PrivacyTypeDTO entity)
        {
            ApplyChange(new PrivacyTypeEvent(entity, ID, Version));
        }
        /// <summary>
        /// 
        /// </summary>
        internal void Remove(PrivacyTypeDTO entity)
        {
            ApplyChange(new PrivacyTypeEvent(entity, ID, Version, null));
        }

        #endregion Internal command implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public void Handle(PrivacyTypeEvent handle)
        {
            ID = handle.AggregateID;
            Version = handle.Version;
            PrivacyType = handle.PrivacyType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memento"></param>
        public void SetMemento(BaseMemento memento)
        {
            ID = memento.ID;
            Version = memento.Version;
            PrivacyType = ((PrivacyTypeBase)memento).PrivacyType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseMemento GetMemento()
        {
            return new PrivacyTypeBase(PrivacyType, ID, Version);
        }
    }
}
