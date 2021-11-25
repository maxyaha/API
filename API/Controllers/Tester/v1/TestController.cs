using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microservice.Application.Entities;
using Microservice.Application.Entities.Tester.Maps;
using Microservice.Application.Entities.Tester.Models;
using Microservice.BusinessLogic;
using Microservice.BusinessLogic.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shareds.Logging.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Tester.v1
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IActionResult actor;
        private readonly ILogger logger;
        private readonly ITestManager manager;
        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="logger"></param>
        public TestController(IConfiguration config, ITestManager manager, ILogger logger)
        {
            this.manager = manager;
            this.logger = logger;
            _config = config;
        }

        // GET: api/<TestController>
        [HttpGet]
        [ProducesResponseType(typeof(Test), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            List<Test> view = null;
            try
            {
                #region Authorize
                /// var authorize = new CustomAuthorize(_config["AppSettings:Secret"], _config["AppSettings:Issuer"]);

                ///  var token = authorize.DecodeJSONWebToken(Request.Headers["ApiUserToken"]);

                ///if (token == null)
                ///{
                ///    actor = new UnauthorizedResult();
                ///}
                ///</summary>
                #endregion

                var queries = await manager.Test().ConfigureAwait(false);
                view = queries.Select(new TestMapper().ToPresentationModel).ToList();

            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("{0}: ", nameof(ex)), ex);
            }
            finally
            {
                Request.Method = HttpMethods.Get;
                Response.Success(ref this.actor, view, Request);
            }
            return actor;
        }

        // GET api/<TestController>/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Test), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            Test view = null;
            try
            {
                #region Authorize
                /// var authorize = new CustomAuthorize(_config["AppSettings:Secret"], _config["AppSettings:Issuer"]);

                ///  var token = authorize.DecodeJSONWebToken(Request.Headers["ApiUserToken"]);

                ///if (token == null)
                ///{
                ///    actor = new UnauthorizedResult();
                ///}
                ///</summary>
                #endregion

                var query = await manager.Test(id).ConfigureAwait(false);
                view = new TestMapper().ToPresentationModel(query);

            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("{0}: ", nameof(ex)), ex);
            }
            finally
            {
                // Request.Method = HttpMethods.Get;
                Response.Success(ref this.actor, view, Request);
            }
            return actor;

        }

        // POST api/<TestController>
        [HttpPost]
        [ProducesResponseType(typeof(Test), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody] Test entity)
        {
            Test view = null;
            try
            {
                #region Authorize
                /// var authorize = new CustomAuthorize(_config["AppSettings:Secret"], _config["AppSettings:Issuer"]);

                ///  var token = authorize.DecodeJSONWebToken(Request.Headers["ApiUserToken"]);

                ///if (token == null)
                ///{
                ///    actor = new UnauthorizedResult();
                ///}
                ///</summary>
                #endregion

                var test = new TestMapper().ToDataTransferObject(entity);
                var query = await manager.AddTest(test).ConfigureAwait(false);
                view = new TestMapper().ToPresentationModel(query);

            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("{0}: ", nameof(ex)), ex);
            }
            finally
            {
                // Request.Method = HttpMethods.Get;
                Response.Success(ref this.actor, view, Request);
            }
            return actor;

        }

        //// PUT api/<TestController>/
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Test), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Test entity)
        {
            Test view = null;
            try
            {
                #region Authorize
                // var authorize = new CustomAuthorize(_config["AppSettings:Secret"], _config["AppSettings:Issuer"]);

                //  var token = authorize.DecodeJSONWebToken(Request.Headers["ApiUserToken"]);

                //if (token == null)
                //{
                //    actor = new UnauthorizedResult();
                //}
             
                #endregion

                var test = new TestMapper().ToDataTransferObject(entity);
                var query = await manager.UpdateTest(id, test).ConfigureAwait(false);
                view = new TestMapper().ToPresentationModel(query);

            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("{0}: ", nameof(ex)), ex);
            }
            finally
            {
                // Request.Method = HttpMethods.Get;
                Response.Success(ref this.actor, view, Request);
            }
            return actor;
        }

        //// DELETE api/<TestController>/
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Test), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                #region Authorize
                // var authorize = new CustomAuthorize(_config["AppSettings:Secret"], _config["AppSettings:Issuer"]);

                //  var token = authorize.DecodeJSONWebToken(Request.Headers["ApiUserToken"]);

                //if (token == null)
                //{
                //    actor = new UnauthorizedResult();
                //}
                //</summary>
                #endregion

                await manager.DeleteTest(id).ConfigureAwait(false);
                
            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("{0}: ", nameof(ex)), ex);
            }
            finally
            {
                // Request.Method = HttpMethods.Get;
                Response.Success(ref this.actor, null, Request);
            }
            return actor;



        }
    }
}
