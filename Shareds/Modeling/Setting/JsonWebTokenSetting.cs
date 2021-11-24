using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Shareds.Modeling.Setting
{
    public class JsonWebTokenSetting
    {
        public string Secret { get; set; }

        public PayloadSetting Payload { get; set; }
    }

    public class PayloadSetting
    {
        private string ip;

        private string formatdate;



        public string systemId { get; set; }

        public string sourceIp { get => String.IsNullOrEmpty(ip) ? GetLocalIPAddress() : ip; set => ip = value; }

        public string timestamp { get => String.IsNullOrEmpty(formatdate) ? null : String.Format(formatdate, DateTime.Now); set => formatdate = value; }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var address in host.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }
}
