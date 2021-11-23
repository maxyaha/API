using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.DataAccress.Entites.Tester.Models
{

    public partial class Test : Master
    {
        public Test()
        {
            InitializePartial();
        }

        partial void InitializePartial();

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        public string Age { get; set; }

    }
}
