
using System.Collections.Generic;

namespace Microservice.DataAccress.Entites.Features.Models
{
    public partial class PrivacyType : Lookup
    {
        public PrivacyType()
        {
            Guides = new HashSet<Guide>();
            News = new HashSet<News>();


            InitializePartial();
        }

        partial void InitializePartial();


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


        public virtual ICollection<Guide> Guides { get; set; }
        public virtual ICollection<News> News { get; set; }

    

    }
}
