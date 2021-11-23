using System;

namespace Shareds.DesignPatterns.CQRS.Events.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Guid AggregateID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DateTime Timestamp { get; set; }
    }
}
