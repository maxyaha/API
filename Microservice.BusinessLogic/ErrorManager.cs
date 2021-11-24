using Microservice.Companion.Entities.Errors.Models;
using Shareds.Logging.Interfaces;
using Shareds.Utilizing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.BusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IErrorManager
    {
        Task<IEnumerable<Error>> Errors();
        Task<Error> Error(HttpStatusCode code);
    }
    /// <summary>
    /// 
    /// </summary>
    public class ErrorManager : IErrorManager
    {

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Error> Error(HttpStatusCode code)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            code = new ErrorDictionary().GetStatusCode(code);

            return code is 0
                ? null
                : new Error(((int)code).ToString(), string.Format("{0} ({1}): {2}", ((int)code).ToString().ToUpper(), code.ToString().SpaceUpperCase(), null), null);
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Error>> Errors()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ErrorDictionary().GetStatusCode().Select(o => new Error(((int)o).ToString(), string.Format("{0} ({1}): {2}", ((int)o).ToString().ToUpper(), o.ToString().SpaceUpperCase(), null), null));
        }
    }
}
