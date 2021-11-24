using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2E_Testing.Models;

namespace API.E2E_Testing.Brokers
{
    public partial class TestApiBroker
    {
        private const string TestRelativeUrl = "api/Test";

        public async Task<TestModel> PostTestAsync(TestModel test) =>
            await this.apiFactoryClient.PostContentAsync(TestRelativeUrl, test);

        public async Task<TestModel> GetTestAsync(Guid testid) =>
            await this.apiFactoryClient.GetContentAsync<TestModel>($"{TestRelativeUrl}/{testid}");

        public async Task<TestModel> DeleteTestAsync(Guid testid) =>
            await this.apiFactoryClient.DeleteContentAsync<TestModel>($"{TestRelativeUrl}/{testid}");
    }
}
