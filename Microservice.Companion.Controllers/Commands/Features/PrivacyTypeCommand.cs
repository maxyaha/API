using Microservice.Companion.Entities.Features.Models;
using Shareds.DesignPatterns.CQRS.Commands;
using System;

namespace Microservice.Companion.Controllers.Commands.Features
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyTypeCommand : Command
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public PrivacyTypeCommand(PrivacyTypeDTO entity) : base(Guid.Empty, -1, CommandState.Added)
        {
            PrivacyType = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public PrivacyTypeCommand(PrivacyTypeDTO entity, Guid id, int version) : base(id, version, CommandState.Changed)
        {
            PrivacyType = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public PrivacyTypeCommand(PrivacyTypeDTO entity, Guid id, int version, DateTime? timestamp) : base(id, version, CommandState.Removed)
        {
            PrivacyType = entity;
        }

        public PrivacyTypeDTO PrivacyType { get; private set; }
    }
}
