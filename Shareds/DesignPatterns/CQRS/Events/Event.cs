using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.Model;
using System;

namespace Shareds.DesignPatterns.CQRS.Events
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum EventState
    {
        /// <summary>
        /// 
        /// </summary>
        Added = 1,
        /// <summary>
        /// 
        /// </summary>
        Changed = 2,
        /// <summary>
        /// 
        /// </summary>
        Removed = 3,
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Event : DataTransferObject, IEvent
    {
        public Event(Guid aggregateID, int version, DateTime timestamp, EventState state)
        {
            AggregateID = aggregateID;
            Version = version;
            Timestamp = timestamp;
            State = state;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid AggregateID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EventState State { get; set; }
    }
}
