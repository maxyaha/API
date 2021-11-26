using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Microservice.Application.Entities.Accounts.Models
{
    public class ResetPassword
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    

        #region Ignore
    
        #endregion Ignore
    }
}
