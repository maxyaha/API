using Shareds.DesignPatterns.Model;
using System;

namespace Microservice.DataAccress.Entites
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseModeler : DomainModel
    {
        /// <summary>
        /// When a record is added, this field is updated with the date and time. 
        /// On subsequent updates, the system uses this information to ensure that the update request includes a matching date and time on this field; if it does not, the update fails.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// When a record is updated, this field is updated with the date and time. 
        /// On subsequent updates, the system uses this information to ensure that the update request includes a matching date and time on this field; if it does not, the update fails.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// A version, system-generated key that identifies the specific transaction within the log system that either created, updated, or deleted the data row.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Active { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class Lookup : BaseModeler
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class Master : BaseModeler
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class Transact : BaseModeler
    {
    }

}
