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
        public TestEvent(TestDTO entity) : base(Guid.Empty, 0, DateTime.Now, EventState.Added)
        {
            Test = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public TestEvent(TestDTO entity, Guid aggregateID, int version) : base(aggregateID, version, DateTime.Now, EventState.Changed)
        {
            Test = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateID"></param>
        /// <param name="version"></param>
        public TestEvent(TestDTO entity, Guid aggregateID, int version, DateTime? timestamp) : base(aggregateID, version, DateTime.Now, EventState.Removed)
        {
            Test = entity;
        }

        public TestDTO Test { get; internal set; }
    }
}
