using System;
using System.Collections.Generic;
using System.Text;
using Microservice.Companion.Entities.Features.Models;
using Newtonsoft.Json;

namespace Microservice.Application.Entities.Tester.Models
{
    public class Test : BaseModeler
    {
        public string Name { get; set; }
        public string Age { get; set; }

        #region Ignore
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public override string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public override PrivacyTypes PrivacyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        #endregion Ignore
    }
}
