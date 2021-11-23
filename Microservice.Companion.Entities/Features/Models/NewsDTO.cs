using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Companion.Entities.Features.Models
{
    [Serializable]
    public class NewsDTO : BaseModeler
    {
        public NewsDTO()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid FeatureID { get; set; }
        public virtual FeatureDTO Feature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid PrivacyTypeID { get; set; }
        public virtual PrivacyTypeDTO PrivacyType { get; set; }
    }
}
