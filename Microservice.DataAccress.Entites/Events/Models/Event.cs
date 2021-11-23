using Shareds.DesignPatterns.Model;
using System;

namespace Microservice.DataAccress.Entites.Events.Models
{
    /// <summary>
    /// The AggregateRoot table contains information on items that are of interest to a every event.
    /// </summary>
    public class Event : DomainModel
    {
        /// <summary>
        /// The identifier that describes what reference of aggregate root this is.
        /// </summary>
        public Guid AggregateID { get; set; }
        /// <summary>
        /// The binary data of the ทemento.
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// When a record is added, this field is updated with the date and time. 
        /// On subsequent updates, the system uses this information to ensure that the update request includes a matching date and time on this field; if it does not, the update fails.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
