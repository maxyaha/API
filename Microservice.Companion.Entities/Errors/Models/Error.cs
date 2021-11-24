using System;

namespace Microservice.Companion.Entities.Errors.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class Error : BaseModeler
    {
        public Error()
        {
        }

        public Error(string code, string message, string error)
        {
            Dictionary.Code = code;
            Dictionary.Message = message;
            Dictionary.InnerError = error;
        }


        /// <summary>
        /// 
        /// </summary>
        public ErrorDictionary Dictionary { get; set; } = new ErrorDictionary();
    }
}
