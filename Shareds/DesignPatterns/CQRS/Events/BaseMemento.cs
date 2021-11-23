using Shareds.DesignPatterns.Model;
using System;

namespace Shareds.DesignPatterns.CQRS.Events
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BaseMemento : DataTransferObject
    {
        public BaseMemento(Guid id, int version)
        {
            ID = id;
            Version = version;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }
    }
}
