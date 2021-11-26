using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Microservice.Application.Entities.Accounts.Models
{
    public class Account : BaseModeler
    {
        public string Username { get; set; }
        public string Password { get; set; }

        #region Ignore
    
        #endregion Ignore
    }
}
