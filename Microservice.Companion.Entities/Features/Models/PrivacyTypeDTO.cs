
using System;
using System.Collections.Generic;

namespace Microservice.Companion.Entities.Features.Models
{
    public enum PrivacyTypes
    {
        Publish = 1,
        Private = 2
    }

    [Serializable]
    public class PrivacyTypeDTO : BaseModeler
    {
        public PrivacyTypeDTO()
        {
            Guides = new HashSet<GuideDTO>();
            News = new HashSet<NewsDTO>();
        

        }

        /// <summary>
        /// The code name of the party type.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// The name of the party type.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the party type.
        /// </summary>
        public string Description { get; set; }


        public virtual ICollection<GuideDTO> Guides { get; set; }
        public virtual ICollection<NewsDTO> News { get; set; }
      
    }
}
