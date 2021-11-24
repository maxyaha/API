using System;
using System.Collections.Generic;
using System.Text;

namespace Shareds.Modeling.Setting
{
    public class IsamUserSetting
    {
        public string ConnectionServiceString { get; set; }

        public IsamAdminSetting Headers { get; set; }
    }

    public class IsamAdminSetting
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
    }

}
