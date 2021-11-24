using Shareds.DesignPatterns.Model;
using System;

namespace Shareds.Logging
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Delegate : DataTransferObject
    {
        public Delegate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Machinename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string URI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestHeaders { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phrase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResponseHeaders { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResponseContent { get; set; }
    }
}
