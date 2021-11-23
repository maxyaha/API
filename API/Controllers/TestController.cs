using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microservice.Application.Entities;
using Microservice.Application.Entities.Tester.Maps;
using Microservice.Application.Entities.Tester.Models;
using Microservice.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Shareds.Logging.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ITestManager manager;

        private HttpResponseObject message =
            //
            new HttpResponseObject(HttpStatusCode.OK);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="logger"></param>
        public TestController(ITestManager manager, ILogger logger)
        {
            this.manager = manager;
            this.logger = logger;
        }
        // GET: api/<TestController>
        [HttpGet]
        public async Task<HttpResponseObject> Get()
        {
            var queries = await manager.Test().ConfigureAwait(false);

            var result = queries.Select(new TestMapper().ToPresentationModel);
            message.Content = result;
            return message;
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public async Task<HttpResponseObject> Get (Guid id)
        {
            var query = await manager.Test(id).ConfigureAwait(false);

            var result = new TestMapper().ToPresentationModel(query);
            message.Content = result;
            return message;
        }

        // POST api/<TestController>
        [HttpPost]
        public async Task<HttpResponseObject> Post([FromBody] Test entity)
        {
            var test = new TestMapper().ToDataTransferObject(entity);
            await manager.AddTest(test).ConfigureAwait(false);
            return message;
        }

        //// PUT api/<TestController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TestController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
