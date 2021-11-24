using System;
using System.Linq;
using System.Net;

namespace Microservice.Companion.Entities.Errors.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class ErrorDictionary : BaseModeler
    {
        public ErrorDictionary()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; } = string.Format("{0}", String.Empty.PadRight(3, 'X')).ToUpper();
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object InnerError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public HttpStatusCode GetStatusCode(HttpStatusCode code)
        {
            return GetStatusCode().SingleOrDefault(o => o == code);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode[] GetStatusCode()
        {
            return new[]
            {
                // 1XX HttpStatusCode.Continue,
                // 2XX
                HttpStatusCode.OK,
                HttpStatusCode.Created,
                HttpStatusCode.NoContent,
                // 3XX HttpStatusCode.Ambiguous,
                // 4XX
                HttpStatusCode.BadRequest,
                HttpStatusCode.Unauthorized,
                HttpStatusCode.NotFound,
                HttpStatusCode.Conflict,
                // 5XX HttpStatusCode.InternalServerError
            };
        }
    }
}
