using System.Threading.Tasks;
using API.Controllers;
using Microservice.Application.Entities.Tester.Models;
using Microservice.BusinessLogic;
using Microsoft.Extensions.Configuration;
using Shareds.Logging.Interfaces;
using Tynamix.ObjectFiller;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microservice.Companion.Entities.Tester.Models;
using Microservice.Application.Entities.Tester.Maps;
using System.Collections.Generic;
using API.Controllers.Tester.v1;

namespace E2E_Testing.Controller
{
    public class TestControllerTest
    {
        readonly TestController _controller;
        private readonly Mock<ITestManager> manager;


        public TestControllerTest()
        {
            var _iconfig = new Mock<IConfiguration>();
            var logger = new Mock<ILogger>();
            this.manager = new Mock<ITestManager>();
            _controller = new TestController(
                              config: _iconfig.Object,
                              manager: manager.Object,
                              logger: logger.Object)
            {
                ControllerContext = new ControllerContext(),
            };
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }
        private Test CreateRandomTest() =>
          new Filler<Test>().Create();

        [Fact]
        public async Task GetTestAsync()
        {
            Test randomTest = CreateRandomTest();
            Test inputTest = randomTest;
            TestDto exportedTest = new TestMapper().ToDataTransferObject(inputTest);
            List<TestDto> tests = new()
            {
                exportedTest
            };
            

            manager.Setup(x => x.Test()).Returns(Task.FromResult(tests as IEnumerable<TestDto>));

            //when
            _controller.ControllerContext.HttpContext.Request.Method = "GET";

            var result = await _controller.Get();

            var okResult = result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.True(okResult is OkObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetTestByIdAsync()
        {
            //given
            Test randomTest = CreateRandomTest();
            Test inputTest = randomTest;
            TestDto exportedTest = new TestMapper().ToDataTransferObject(inputTest);

            manager.Setup(x => x.Test(inputTest.ID)).Returns(Task.FromResult(exportedTest));

            //when
            _controller.ControllerContext.HttpContext.Request.Method = "GET";

            var result = await _controller.Get(inputTest.ID);

            var okResult = result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.True(okResult is OkObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        }

        [Fact]
        public async Task PostTestAsync()
        {
            Test randomTest = CreateRandomTest();
            Test inputTest = randomTest;
            TestDto exportedTest = new TestMapper().ToDataTransferObject(inputTest);

            _controller.ControllerContext.HttpContext.Request.Method = "POST";

            manager.Setup(x => x.AddTest(exportedTest)).Returns(Task.FromResult(exportedTest));
            var result = await _controller.Post(inputTest);

            var okResult = result as ObjectResult;

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IActionResult>(result);
            //Assert.True(okResult is OkObjectResult);
            //Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
