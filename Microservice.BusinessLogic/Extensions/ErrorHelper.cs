using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Repositories;
using Shareds.Excepting.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Microservice.BusinessLogic.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorHelper
    {
        public static void ErrorStandard(HttpStatusCode code, object error, ref IActionResult action)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    // 400 (BadRequest): The request could not be understood by the server.
                    action = new BadRequestObjectResult(error);
                    break;
                case HttpStatusCode.Unauthorized:
                    // 401 (Unauthorized):
                    action = new UnauthorizedResult();
                    break;
                case HttpStatusCode.NotFound:
                    // 404 (NotFound): The requested resource does not exist on the server.
                    action = new NotFoundObjectResult(error);
                    break;
                case HttpStatusCode.Conflict:
                    // 409 (Conflict): The request could not be carried out because of a conflict on the server.
                    action = new ConflictObjectResult(error);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsError(this IActionResult action)
        {
            return action is BadRequestObjectResult
                || action is UnauthorizedResult
                || action is NotFoundObjectResult
                || action is TimeoutObjectResult
                || action is ConflictObjectResult
                || action is PreconditionFailedObjectResult
                || action is UnsupportedMediaTypeResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ok"></param>
        /// <param name="actor"></param>
        public static void ParseError(this OkObjectResult ok, ref IActionResult actor)
        {
            actor = actor.IsError()
                ? actor
                : ok;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="created"></param>
        /// <param name="actor"></param>
        public static void ParseError(this CreatedResult created, ref IActionResult actor)
        {
            actor = actor.IsError()
                ? actor
                : created;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nocontent"></param>
        /// <param name="actor"></param>
        public static void ParseError(this NoContentResult nocontent, ref IActionResult actor)
        {
            actor = actor.IsError()
                ? actor
                : nocontent;
        }
        /// <summary>
        /// 4xx (Client Error): The request contains bad syntax or cannot be fulfilled.
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="action"></param>
        /// <param name="error"></param>
        public static void Error(this Exception inner, ref IActionResult action, object error)
        {
            switch (inner)
            {
                case ConcurrencyException ex:
                    // 400 (BadRequest): The request could not be understood by the server.
                    action = new BadRequestObjectResult(error);
                    break;
                case UnregisteredDomainCommandException ex:
                    // 400 (BadRequest): The request could not be understood by the server.                    
                    action = new BadRequestObjectResult(error);
                    break;
                case NotImplementedException ex:
                    // 404 (NotFound): The requested resource does not exist on the server.
                    action = new NotFoundObjectResult(error);
                    break;
                case ArgumentNullException ex:
                    // 409 (Conflict): The request could not be carried out because of a conflict on the server.
                    action = new ConflictObjectResult(error);
                    break;
                case InvalidOperationException ex:
                    // 409 (Conflict): The request could not be carried out because of a conflict on the server.
                    action = new ConflictObjectResult(error);
                    break;
                case ValidationException ex:
                    // 409 (Conflict): The request could not be carried out because of a conflict on the server.
                    action = new ConflictObjectResult(error);
                    break;
                case TimeoutException ex:
                    // 408 (Timeout): 
                    action = new TimeoutObjectResult(error);
                    break;
                case PreconditionFailedException ex:
                    // 412 (PreconditionFailed): 
                    action = new PreconditionFailedObjectResult(error);
                    break;
                case Exception ex:
                    // 409 (Conflict): The request could not be carried out because of a conflict on the server.
                    action = new ConflictObjectResult(error);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 2xx
        /// </summary>
        /// <param name="response"></param>
        /// <param name="action"></param>
        /// <param name="succress"></param>
        /// <param name="request"></param>
        public static void Success(this HttpResponse response, ref IActionResult action, object succress, HttpRequest request)
        {
            Success(response, ref action, succress, request.Method);
        }
        /// <summary>
        /// 2xx
        /// </summary>
        /// <param name="response"></param>
        /// <param name="action"></param>
        /// <param name="succress"></param>
        /// <param name="method"></param>
        public static void Success(this HttpResponse response, ref IActionResult action, object succress, string method)
        {
            if (action.IsError())
                return;

            if (succress is null)
            {
                action = new OkResult();
            }
            else
            {
                action = new OkObjectResult(succress);
            }
     
        }
        /// <summary>
        /// 2xx
        /// </summary>
        /// <param name="request"></param>
        /// <param name="action"></param>
        /// <param name="succress"></param>
        public static void Success(this HttpRequest request, ref IActionResult action, object succress)
        {
            if (action.IsError())
                return;

            string method = request.Method;

            if (HttpMethods.IsConnect(method))
                action = null;
            else if (HttpMethods.IsDelete(method))
                action = new NoContentResult();
            else if (HttpMethods.IsGet(method))
                action = new OkObjectResult(succress);
            else if (HttpMethods.IsHead(method))
                action = null;
            else if (HttpMethods.IsOptions(method))
                action = null;
            else if (HttpMethods.IsPatch(method))
                action = new PartialViewResult();
            else if (HttpMethods.IsPost(method))
                action = new CreatedResult(request.Path, succress);
            else if (HttpMethods.IsPut(method))
                action = new CreatedResult(request.Path, succress);
            else if (HttpMethods.IsTrace(method))
                action = null;
        }
        /// <summary>
        /// ลอง error
        /// </summary>
        /// <param name="code"></param>
        public static void WrongForced(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.BadRequest:
                    // 400 (BadRequest): The request could not be understood by the server.
                    throw new ConcurrencyException("The system is forced to receive an error.");
                //case HttpStatusCode.Unauthorized:
                //    // 401 (Unauthorized):
                ///    throw new ConcurrencyException();
                case HttpStatusCode.NotFound:
                    // 404 (NotFound): The requested resource does not exist on the server.
                    throw new NotImplementedException("The system is forced to receive an error.");
                case HttpStatusCode.Conflict:
                    // 409 (Conflict): The request could not be carried out because of a conflict on the server.
                    throw new Exception("The system is forced to receive an error.");
                default:
                    break;
            }
        }
        /// <summary>
        /// ลอง succress
        /// </summary>
        /// <param name="request"></param>
        /// <param name="code"></param>
        public static void CorrectForced(this HttpRequest request, HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    // 200 (OK):
                    request.Method = HttpMethods.Get;
                    break;
                case HttpStatusCode.Created:
                    // 201 (Created):
                    request.Method = HttpMethods.Post;
                    break;
                case HttpStatusCode.NoContent:
                    // 204 (NoContent):
                    request.Method = HttpMethods.Delete;
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// An <see cref="ObjectResult"/> that when executed will produce a Request Timeout (408) response.
        /// </summary>
        internal class TimeoutObjectResult : ObjectResult
        {
            private const int DefaultStatusCode = StatusCodes.Status408RequestTimeout;

            /// <summary>
            /// Creates a new <see cref="TimeoutObjectResult"/> instance.
            /// </summary>
            /// <param name="error">Contains the errors to be returned to the client.</param>
            public TimeoutObjectResult(object error) : base(error)
            {
                StatusCode = DefaultStatusCode;
            }
        }
        /// <summary>
        /// An <see cref="ObjectResult"/> that when executed will produce a Request Precondition Failed (412) response.
        /// </summary>
        internal class PreconditionFailedObjectResult : ObjectResult
        {
            private const int DefaultStatusCode = StatusCodes.Status412PreconditionFailed;

            /// <summary>
            /// Creates a new <see cref="PreconditionFailedObjectResult"/> instance.
            /// </summary>
            /// <param name="error">Contains the errors to be returned to the client.</param>
            public PreconditionFailedObjectResult(object error) : base(error)
            {
                StatusCode = DefaultStatusCode;
            }
        }
    }
}
