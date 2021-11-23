using Microservice.Companion.Controllers.Commands.Tester;
using Microservice.Companion.Entities.Tester.Maps;
using Microservice.Companion.Entities.Tester.Models;
using Microservice.DataAccress.Entites.Tester.Models;
using Microservice.DataAccress.Tester.Repositories;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservice.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITestManager
    {

        Task<IEnumerable<TestDTO>> Test();
        Task<TestDTO> Test(Guid? id);
        Task<TestDTO> AddTest(TestDTO entity);

    }

    public class TestManager : ITestManager
    {
        private readonly ITestRepositoryAsync repoTest;
        private readonly ICommandBus command;




        public TestManager(ITestRepositoryAsync repoTest
            , ICommandBus command)
        {
            this.repoTest = repoTest;
            this.command = command;

        }

        public async Task<IEnumerable<TestDTO>> Test()
        {
            var queries = await this.repoTest.GetAllAsync().ConfigureAwait(false);

            return queries.Select(new TestMapper().ToDataTransferObject);
        }
        public async Task<TestDTO> Test(Guid? id)
        {
            var query = await this.repoTest.GetSingleAsync(x => x.ID == id).ConfigureAwait(false);

            return new TestMapper().ToDataTransferObject(query);
        }

        public async Task<TestDTO> AddTest(TestDTO entity)
        {
            Command handle;

            handle = new TestCommand(entity);
            await this.command.Send(handle as TestCommand).ConfigureAwait(false);
           
            return null;
        }

    }
}
