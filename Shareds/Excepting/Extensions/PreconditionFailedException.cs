using System;
using System.Runtime.Serialization;

namespace Shareds.Excepting.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class PreconditionFailedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public PreconditionFailedException()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PreconditionFailedException(string message) : base(message)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public PreconditionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        private PreconditionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
