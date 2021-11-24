using Shareds.Formatting;
using System.Collections.Generic;

namespace Shareds.Authentication
{
    public class FaContext
    {
        public FaContext()
        {
        }

        public FaContext(FaContextOptions options)
        {
            this.Validation = options.Validation.ToDictionary().ToObject<Validation>();
            this.Claimation = options.Claimation.ToDictionary();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual Validation Validation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Dictionary<string, string> Claimation { get; set; }
    }

    public class Validation
    {
        public string Server { get; set; }
        public string Authbase { get; set; }
        public string Permissions { get; set; }
        public bool SigningKey { get; set; }
        public bool Issuer { get; set; }
        public bool Audience { get; set; }
        public bool Lifetime { get; set; }
        public int Delay { get; set; }
        public int Timeout { get; set; }
    }
}
