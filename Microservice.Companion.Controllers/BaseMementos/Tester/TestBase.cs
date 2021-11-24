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
        public TestBase(TestDto entity, Guid id, int version) : base(id, version)
        {
            Test = entity;
        }

        public TestDto Test { get; set; }
    }
}
