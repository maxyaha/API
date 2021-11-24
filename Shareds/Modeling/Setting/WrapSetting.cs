using System;
using System.Collections.Generic;
using System.Text;

namespace Shareds.Modeling.Setting
{
    public class WrapSetting
    {
        public string ConnectionServiceString { get; set; }

        public HeadersSetting Headers { get; set; }
    }

    public class HeadersSetting
    {
        public string ApiToken { get; set; }

        public string SystemId { get; set; }

    }
}
