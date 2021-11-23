using Microservice.Companion.Entities.Tester.Models;
using Shareds.DesignPatterns.CQRS.Events;
using System;

namespace Microservice.Companion.Controllers.BaseMementos.Tester
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TestBase : BaseMemento
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public TestBase(TestDTO entity, Guid id, int version) : base(id, version)
        {
            Test = entity;
        }

        public TestDTO Test { get; set; }
    }
}
