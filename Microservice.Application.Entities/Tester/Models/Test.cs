using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Microservice.Application.Entities.Tester.Models
{
    public class Test : BaseModeler
    {
        public string Name { get; set; }
        public string Age { get; set; }

        #region Ignore
    
        #endregion Ignore
    }
}
