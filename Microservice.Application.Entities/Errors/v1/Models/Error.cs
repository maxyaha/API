
using Newtonsoft.Json;
using Shareds.Utilizing;
using System;
using System.Net;

namespace Microservice.Application.Entities.Errors.v1.Models
{
    public partial class Error : BaseModeler
    {
        public Error()
        {
        }

        public Error(string code, string message, object error)
        {
            Dictionary.Code = code;
            Dictionary.Message = message;
            Dictionary.InnerError = error;
        }

        public Error(ApiStatusCode api, string message, object error, HttpStatusCode code)
        {
            Dictionary.Code = ((int)api).ToString();
            Dictionary.Message = string.Format("{0} ({1}): {2}", ((int)code).ToString().ToUpper(), code.ToString().SpaceUpperCase(), message);
            Dictionary.InnerError = error;
        }

        public Error(HttpStatusCode code, string message, object error)
        {
            Dictionary.Code = ((int)code).ToString();
            Dictionary.Message = string.Format("{0} ({1}): {2}", ((int)code).ToString().ToUpper(), code.ToString().SpaceUpperCase(), message);
            Dictionary.InnerError = error;
        }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("error")]
        public ErrorDictionary Dictionary { get; set; } = new ErrorDictionary();

        #region Ignore
        /// <summary>
        /// Ignore key that identifies.
        /// </summary>
        [JsonIgnore]
        public override Guid ID { get; set; }
        /// <summary>
        /// Ignore the date of created.
        /// </summary>
        [JsonIgnore]
        public override DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Ignore the date of modified.
        /// </summary>
        [JsonIgnore]
        public override DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Ignore the version.
        /// </summary>
        [JsonIgnore]
        public override int Version { get; set; }


        #endregion Ignore

    }
}
