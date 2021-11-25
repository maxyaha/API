using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;
using System.Threading;
using Xunit;
using Microservice.BusinessLogic;
using Microservice.DataAccress.Tester.Repositories;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Microservice.DataAccress.Entites.Tester.Models;
using Tynamix.ObjectFiller;
using Microservice.Companion.Controllers.Commands.Tester;
using Shareds.DesignPatterns.CQRS.Commands;
using Microservice.Companion.Entities.Tester.Maps;
using Microservice.Companion.Entities.Tester.Models;

namespace E2E_Testing.Manager
{
    public class TestManagerTest
    {
        private readonly TestManager manager;
        private readonly Mock<ITestRepositoryAsync> repoTest;
        private readonly Mock<ICommandBus> command;

        public TestManagerTest()
        {
            repoTest = new Mock<ITestRepositoryAsync>();
            command = new Mock<ICommandBus>();

            manager = new TestManager(
                repoTest: repoTest.Object,
                command: command.Object
                );
        }

        public static TheoryData<Expression<Func<Test, bool>>> Get_Id = new()
        {
            {
                (x => x.ID == Guid.Empty)
            },
        };

        private static Test CreateRandomTest() =>
                new Filler<Test>().Create();



        [Theory]
        [MemberData(nameof(Get_Id))]
        public async Task GetId(Expression<Func<Test, bool>> expression)
        {
            Test test = CreateRandomTest();
            List<Test> tests = new()
            {
                test
            };

            CancellationToken token = CancellationToken.None;

            //expression = x => x.Active;
            Expression<Func<Test, bool>> predicatesTest = x => x.Active;

            repoTest.Setup(setup => setup.GetAsync(It.IsAny<Func<IQueryable<Test>, IQueryable<Test>>>(), token))
                       .ReturnsAsync(tests.Where(expression.Compile()));

            var result = await manager.Test(test.ID);

            Assert.NotNull(result);
            Assert.Equal(test.Name, result.Name);

        }

        [Fact]
        public async Task GetAll()
        {
            Test test = CreateRandomTest();
            List<Test> tests = new()
            {
                test
            };

            CancellationToken token = CancellationToken.None;

            //expression = x => x.Active;
            Expression<Func<Test, bool>> predicatesTest = x => x.Active;

            repoTest.Setup(setup => setup.GetAsync(It.IsAny<Func<IQueryable<Test>, IQueryable<Test>>>(), token))
                       .ReturnsAsync(tests);

            var result = await manager.Test();

            Assert.NotNull(result);

        }

        [Fact]
        public async Task Add()
        {
            TestDto test = new TestMapper().ToDataTransferObject(CreateRandomTest());

            Command handle;
            CancellationToken token = CancellationToken.None;

            handle = new TestCommand(test);
            command.Setup(x => x.Send(handle as TestCommand)).Verifiable();

            var result = await manager.AddTest(test);

            Assert.NotNull(result);
            Assert.Equal(result.Name,test.Name);

        }
    }
}