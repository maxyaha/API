using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microservice.Application.AppStart;
using Microservice.Application.Entities;

namespace xUnit_Testing
{
    public  class SourceIp
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static string Token()
        {
            var jwt = new JsonWebToken
            {
                UserID = Guid.NewGuid().ToString(),
            };

            var authorize = new CustomAuthorize("ThisismySecretKey", "Max");
            jwt.Token = authorize.GenerateJSONWebToken(jwt);
            return jwt.Token;
        }
    }
}
