using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.DataAccress.Entites.Features.Models
{
    public partial class Guide : Master
    {
        public Guide()
        {
            InitializePartial();
        }

        partial void InitializePartial();

        /// <summary>
        /// 
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid FeatureID { get; set; }
        public virtual Feature Feature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid PrivacyTypeID { get; set; }
        public virtual PrivacyType PrivacyType { get; set; }


    }
}
