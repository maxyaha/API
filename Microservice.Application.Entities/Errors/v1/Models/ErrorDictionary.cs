
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Microservice.Application.Entities.Errors.v1.Models
{
    public partial class ErrorDictionary : BaseModeler
    {
        public ErrorDictionary()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(3)]
        [JsonProperty("code")]
        public string Code { get; set; } = String.Format("{0}", String.Empty.PadRight(3, 'X')).ToUpper();
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("innerError")]
        public object InnerError { get; set; }

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
