using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Shareds.Setting
{
    public class PhoenixSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Payload Payload { get; set; }
    }

    public class Payload
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
            throw new InvalidOperationException("No network adapters with an IPv4 address in the system!");
        }
    }


    public static class PhoenixServices
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services { get { return services; } set { services = services is null ? value : throw new Exception("Can't set once a value has already been set."); } }
        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext Context { get { return (services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor)?.HttpContext; } }
        /// <summary>
        /// 
        /// </summary>
        public static IHostingEnvironment HostingEnvironment { get { return services.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment; } }
        /// <summary>
        /// Configuration settings from appsetting.json.
        /// </summary>
        public static PhoenixSettings Config { get { return (services.GetService(typeof(IOptionsMonitor<PhoenixSettings>)) as IOptionsMonitor<PhoenixSettings>).CurrentValue; } }
    }
}
