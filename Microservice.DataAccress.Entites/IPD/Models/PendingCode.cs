using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.DataAccress.Entites.IPD.Models
{
    public class PendingCode : Master
    {
        public string Code { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionTH { get; set; }
        public string Flag { get; set; }
    }
}
