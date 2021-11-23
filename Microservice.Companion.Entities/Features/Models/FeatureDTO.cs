using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Companion.Entities.Features.Models
{
    [Serializable]
    public class FeatureDTO : BaseModeler
    {
        public FeatureDTO()
        {
            Guides = new HashSet<GuideDTO>();
            News = new HashSet<NewsDTO>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Topics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<GuideDTO> Guides { get; set; }
        public virtual ICollection<NewsDTO> News { get; set; }
    }
}
