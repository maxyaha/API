using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2E_Testing.APIs.Tester;
using Xunit;

namespace E2E_Testing.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public  class ApiTestCollection:ICollectionFixture<TestApiTests>   
    {
    }
}
