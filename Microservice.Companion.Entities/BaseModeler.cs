using Shareds.DesignPatterns.Model;
using System;

namespace Microservice.Companion.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseModeler : DataTransferObject
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }
    }
}
