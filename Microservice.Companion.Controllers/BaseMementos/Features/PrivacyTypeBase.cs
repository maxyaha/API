using Microservice.Companion.Entities.Features.Models;
using Shareds.DesignPatterns.CQRS.Events;
using System;

namespace Microservice.Companion.Controllers.BaseMementos.Features
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PrivacyTypeBase : BaseMemento
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public PrivacyTypeBase(PrivacyTypeDTO entity, Guid id, int version) : base(id, version)
        {
            PrivacyType = entity;
        }

        public PrivacyTypeDTO PrivacyType { get; set; }
    }
}
