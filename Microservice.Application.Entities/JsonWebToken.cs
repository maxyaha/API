
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice.Application.Entities
{
    public class JsonWebToken
    {
        public JsonWebToken()
        {
        }

        public string UserID { get; set; }

        public string Token { get; set; }

    }
}
