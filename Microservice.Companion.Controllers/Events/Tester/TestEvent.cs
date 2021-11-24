using Microservice.Companion.Entities.Tester.Models;
using Shareds.DesignPatterns.CQRS.Events;
using System;

namespace Microservice.Companion.Controllers.Events.Tester
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TestEvent : Event
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public TestEvent(TestDto entity) : base(Guid.Empty, 0, DateTime.Now, Eventstates.Added)
        {
            Test = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public TestEvent(TestDto entity, Guid aggregateID, int version) : base(aggregateID, version, DateTime.Now, Eventstates.Changed)
        {
            Test = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public TestEvent(TestDto entity, Guid aggregateID, int version, bool active) : base(aggregateID, version, DateTime.Now, Eventstates.Removed)
        {
            Test = entity;
        }

        public TestDto Test { get; internal set; }
    }
}
