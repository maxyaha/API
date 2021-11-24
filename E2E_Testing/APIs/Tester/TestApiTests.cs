using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2E_Testing.Brokers;
using E2E_Testing.Models;
using FluentAssertions;
using Tynamix.ObjectFiller;
using Xunit;

namespace E2E_Testing.APIs.Tester
{
    [Collection(nameof(ApiTestCollection))]
    public class TestApiTests
    {
        private readonly TestApiBroker _testApiBroker;

        public TestApiTests(TestApiBroker testApiBroker)
        {
            this._testApiBroker = testApiBroker;
        }

        private TestModel CreateRandomTest() =>
            new Filler<TestModel>().Create();

        [Fact]
        public async Task ShouldPostTestAsync()
        {
            //Given
            TestModel randomTest = CreateRandomTest();
            TestModel inputTest = randomTest;
            TestModel exportedTest = inputTest;

            //when
            TestModel actualTest =
                await _testApiBroker.PostTestAsync(inputTest);

            actualTest.Should().BeEquivalentTo(exportedTest);

            await _testApiBroker.DeleteTestAsync(actualTest.Id);
        }
    }
}
