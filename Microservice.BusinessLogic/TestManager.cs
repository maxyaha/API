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

        Task<IEnumerable<TestDto>> Test();
        Task<TestDto> Test(Guid? id);
        Task<TestDto> AddTest(TestDto entity);
        Task<TestDto> UpdateTest(Guid id,TestDto entity);
        Task<TestDto> DeleteTest(Guid id);

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

        public async Task<IEnumerable<TestDto>> Test()
        {
            var queries = await this.repoTest.GetAllAsync().ConfigureAwait(false);

            return queries.Select(new TestMapper().ToDataTransferObject);
        }
        public async Task<TestDto> Test(Guid? id)
        {
            var query = await this.repoTest.GetSingleAsync(x => x.ID == id).ConfigureAwait(false);

            return new TestMapper().ToDataTransferObject(query);
        }

        public async Task<TestDto> AddTest(TestDto entity)
        {
            Command handle;

            handle = new TestCommand(entity);
            await this.command.Send(handle as TestCommand).ConfigureAwait(false);
           
            return entity;
        }

        public async Task<TestDto> UpdateTest(Guid id, TestDto entity)
        {
            var query = await this.repoTest.GetSingleAsync(x => x.ID == id).ConfigureAwait(false);
            var map = new TestMapper().ToDataTransferObject(query);

            entity.ModifiedDate = map.ModifiedDate;
            entity.CreatedDate = map.CreatedDate;
            entity.Version = map.Version;
            

            Command handle;

            handle = new TestCommand(entity, id, entity.Version);
            await this.command.Send(handle as TestCommand).ConfigureAwait(false);

            return entity;
        }

        public async Task<TestDto> DeleteTest(Guid id)
        {
            var query = await this.repoTest.GetSingleAsync(x => x.ID == id).ConfigureAwait(false);
            var map = new TestMapper().ToDataTransferObject(query);

            Command handle;

            handle = new TestCommand(map, id, map.Version,false);
            await this.command.Send(handle as TestCommand).ConfigureAwait(false);

            return map;
        }
    }
}
