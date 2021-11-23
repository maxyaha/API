using Microservice.Companion.Controllers.BaseMementos.Tester;
using Microservice.Companion.Controllers.Events.Tester;
using Microservice.Companion.Entities.Tester.Models;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using System;

namespace Microservice.Companion.Controllers.AggregateRoots.Tester
{
    /// <summary>
    /// 
    /// </summary>
    public class TestRoot : AggregateRoot, IHandle<TestEvent>, IOriginator
    {
        public TestRoot()
        {
        }

        public TestDTO Test { get; private set; }

        #region Internal command implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        internal TestRoot(Guid id, TestDTO entity)
        {
            ApplyChange(new TestEvent(entity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        internal void Change(TestDTO entity)
        {
            ApplyChange(new TestEvent(entity, ID, Version));
        }
        /// <summary>
        /// 
        /// </summary>
        internal void Remove(TestDTO entity)
        {
            ApplyChange(new TestEvent(entity, ID, Version, null));
        }

        #endregion Internal command implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        public void Handle(TestEvent handle)
        {
            ID = handle.AggregateID;
            Version = handle.Version;
            Test = handle.Test;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memento"></param>
        public void SetMemento(BaseMemento memento)
        {
            ID = memento.ID;
            Version = memento.Version;
            Test = ((TestBase)memento).Test;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BaseMemento GetMemento()
        {
            return new TestBase(Test, ID, Version);
        }
    }
}
