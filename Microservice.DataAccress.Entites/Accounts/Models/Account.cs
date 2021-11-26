using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.DataAccress.Entites.Accounts.Models
{
    public partial class Account : Master
    {
        public Account()
        {
            InitializePartial();
        }

        partial void InitializePartial();

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
