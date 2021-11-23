using Shareds.DesignPatterns.Model;

namespace Microservice.DataAccress.Entites.Events.Models
{
    /// <summary>
    /// The AggregateRoot table contains information on items that are of interest to a current events.
    /// </summary>
    public class AggregateRoot : DomainModel
    {
        /// <summary>ฃ
        /// A version of event this is, system-generated key that identifies the specific transaction within the log system that either created, updated, or deleted the data row.
        /// </summary>
        public int EventVersion { get; set; }
        /// <summary>
        /// A version, system-generated key that identifies the specific transaction within the log system that either created, updated, or deleted the data row.
        /// </summary>
        public int Version { get; set; }
    }
}
