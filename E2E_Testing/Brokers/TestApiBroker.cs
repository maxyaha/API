using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using E2E_Testing.Models;
using Microservice.Application;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;

namespace E2E_Testing.Brokers
{
    public partial class TestApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public TestApiBroker()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.httpClient = new HttpClient();
            this.apiFactoryClient = new RESTFulApiFactoryClient(this.httpClient);
        }

        private const string TestRelativeUrl = "/api/Test";

        public async ValueTask<TestModel> PostTestAsync(TestModel test) =>
            await this.apiFactoryClient.PostContentAsync(TestRelativeUrl, test);

        public async ValueTask<TestModel> GetTestAsync(Guid testid) =>
            await this.apiFactoryClient.GetContentAsync<TestModel>($"{TestRelativeUrl}/{testid}");

        public async ValueTask<TestModel> DeleteTestAsync(Guid testid) =>
            await this.apiFactoryClient.DeleteContentAsync<TestModel>($"{TestRelativeUrl}/{testid}");
    }
}
