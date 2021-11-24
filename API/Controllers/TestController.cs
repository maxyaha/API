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

namespace API.Controllers
{
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


        private HttpResponseObject message =
            //
            new HttpResponseObject(HttpStatusCode.OK);

        // GET: api/<TestController>
        [HttpGet]
    //    [ProducesResponseType(typeof(Test), (int)HttpStatusCode.OK)]
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

        // GET api/<TestController>/5
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
                view = new TestMapper().ToPresentationModel(await manager.AddTest(test).ConfigureAwait(false));

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

        //// PUT api/<TestController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Test entity)
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
                view = new TestMapper().ToPresentationModel(await manager.UpdateTest(id, test).ConfigureAwait(false));

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

        //// DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
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

                view = new TestMapper().ToPresentationModel(await manager.DeleteTest(id).ConfigureAwait(false));

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
    }
}
