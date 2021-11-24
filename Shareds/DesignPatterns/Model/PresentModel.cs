using System;
using System.Collections.Generic;
using System.Text;

namespace Shareds.DesignPatterns.Model
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PresentModel : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public virtual void RelayLoading(object source)
        { }


        /// <summary>
        /// 
        /// </summary>
        public virtual int Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? CreatedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }
    }

    public abstract class PresentModelWrapper
    {
      
    }
}
