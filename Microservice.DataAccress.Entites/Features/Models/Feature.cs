using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.DataAccress.Entites.Features.Models
{
    public partial class Feature : Master
    {
        public Feature()
        {
            Guides = new HashSet<Guide>();
            News = new HashSet<News>();


            InitializePartial();
        }

        partial void InitializePartial();

        /// <summary>
        /// 
        /// </summary>
        public string Topics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<Guide> Guides { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
