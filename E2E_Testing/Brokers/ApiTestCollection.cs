using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.E2E_Testing.APIs.Tester;
using Xunit;

namespace API.E2E_Testing.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<TestApiBroker>
    {
    }
}
