using System;

namespace Microservice.Companion.Entities.Accounts.Models
{
    [Serializable]
    public class AccountDto : BaseModeler
    {
        public AccountDto()
        {
          
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
