using Microservice.Companion.Entities.Tester.Models;
using Shareds.DesignPatterns.CQRS.Commands;
using System;

namespace Microservice.Companion.Controllers.Commands.Tester
{
    /// <summary>
    /// 
    /// </summary>
    public class TestCommand : Command
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public TestCommand(TestDTO entity) : base(Guid.Empty, -1, CommandState.Added)
        {
            Test = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public TestCommand(TestDTO entity, Guid id, int version) : base(id, version, CommandState.Changed)
        {
            Test = entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public TestCommand(TestDTO entity, Guid id, int version, DateTime? timestamp) : base(id, version, CommandState.Removed)
        {
            Test = entity;
        }

        public TestDTO Test { get; private set; }
    }
}
