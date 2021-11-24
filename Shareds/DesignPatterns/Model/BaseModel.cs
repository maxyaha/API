using System;
using System.Collections.Generic;
using System.Text;

namespace Shareds.DesignPatterns.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class BaseModel
    {
        /// <summary>
        /// A unique, system-generated key that identifies in the system.
        /// </summary>
        public virtual Guid ID { get; set; }
    }
}
