using Shareds.DesignPatterns.Model;

namespace Microservice.DataAccress.Entites.Events.Models
{
    /// <summary>
    /// The BaseMemento table contains information on items that are of interest to a export data version.
    /// </summary>
    public class BaseMemento : DomainModel
    {
        /// <summary>
        /// The code of the ทemento.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// A version, system-generated key that identifies the specific transaction within the log system that either created, updated, or deleted the data row.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// The binary data of the ทemento.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
