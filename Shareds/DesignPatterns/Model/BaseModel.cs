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
        public Guid ID { get; set; }
    }
}
