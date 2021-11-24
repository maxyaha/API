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
        public TestCommand(TestDto entity) : base(Guid.Empty, -1, Commandstates.Added)
        {
            Test = entity;
            Test.Active = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public TestCommand(TestDto entity, Guid id, int version) : base(id, version, Commandstates.Changed)
        {
            Test = entity;
            Test.Active = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        public TestCommand(TestDto entity, Guid id, int version, bool active) : base(id, version, Commandstates.Removed)
        {
            Test = entity;
            Test.Active = active;
        }

        public TestDto Test { get; private set; }
    }
}
