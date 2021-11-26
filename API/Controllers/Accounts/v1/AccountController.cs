using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microservice.Application.AppStart;
using Microservice.Application.Entities;
using Microservice.Application.Entities.Accounts.Maps;
using Microservice.Application.Entities.Accounts.Models;
using Microservice.BusinessLogic;
using Microservice.BusinessLogic.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shareds.Logging.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Accounts.v1
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IActionResult actor;
        private readonly ILogger logger;
        private readonly IAccountManager manager;
        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="logger"></param>
        public AccountController(IConfiguration config, IAccountManager manager, ILogger logger)
        {
            this.manager = manager;
            this.logger = logger;
            _config = config;
        }


        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login([FromBody] Account entity)
        {
            object view = null;
            try
            {

                JsonWebToken jwt = new JsonWebToken();
                var query = await manager.Accounts(entity.Username, entity.Password).ConfigureAwait(false);
                if (query is not null)
                {
                    var authorize = new CustomAuthorize(_config["AppSettings:Secret"], _config["AppSettings:Issuer"]);
                    jwt.UserID = query.Username;
                    jwt.Token = authorize.GenerateJSONWebToken(jwt);

                    view = jwt;
                }
                else
                {
                    actor = new NoContentResult();
                }


            }
            catch (Exception ex)
            {
                this.logger.Error(string.Format("{0}: ", nameof(ex)), ex);
            }
            finally
            {
                Request.Method = HttpMethods.Get;
                Request.Success(ref this.actor, view);
                //Response.Success(ref this.actor, view, Request);
            }
            return actor;

        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody] Account entity)
        {
            Account view = null;
            try
            {


                var Account = new AccountMapper().ToDataTransferObject(entity);
                var query = await manager.AddAccounts(Account).ConfigureAwait(false);
                view = new AccountMapper().ToPresentationModel(query);

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

        //// PUT api/<AccountController>/
        [HttpPut]
        [Route("ChangePassword")]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody] ResetPassword entity)
        {
            Account view = null;
            try
            {
                #region Authorize
                var authorize = new CustomAuthorize(_config["AppSettings:Secret"],
                                                    _config["AppSettings:Issuer"]);

                var token = authorize.DecodeJSONWebToken(Request.Headers["ApiUserToken"]);

                if (token == null)
                {
                    actor = new UnauthorizedResult();
                }

                #endregion
                var query = await manager.Accounts(token.UserID).ConfigureAwait(false);
                query.Password = entity.Password;
                await manager.UpdateAccounts(query).ConfigureAwait(false);

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
