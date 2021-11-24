using Shareds.DesignPatterns.Model;
using System;

namespace Microservice.Companion.Entities
{
    public interface IValidatable
    {
        bool IsValid();
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseModeler : DataTransferObject
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreatedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool Active { get; set; }
    }

    [Serializable]
    public abstract class BaseModelerWrapper : DataTransferObjectWrapper
    {

    }
}
