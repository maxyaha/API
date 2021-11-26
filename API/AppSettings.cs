using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public PayloadAppSetting Payload { get; set; }
    }

    public class PayloadAppSetting
    {
        public string userID { get; set; }

        public string userName { get; set; }

        public string role { get; set; }

        public DateTime? Expires { get; set; }
    }
}
