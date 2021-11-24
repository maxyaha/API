using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shareds.Authentication.Extensions
{
    public static class SadServerFaContextOptionsExtensions
    {
        static IConfiguration configuration = null;

        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Configuration { get { return configuration; } set { configuration = value ?? throw new Exception("Can't set once a value has already been set."); } }
        /// <summary>
        /// CONFIGURATION SETTINGS FROM appsetting.json.
        /// </summary>
        public static FaContextOptionsBuilder Builder { get { return configuration.Get<FaContextOptionsBuilder>(); } }

        /// <summary>
        /// STRONG (CUSTOMER) AUTHENTICATION DEFINITIONS
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authorizationstring"></param>
        /// <returns></returns>
        public static FaContextOptionsBuilder UseSadServer(this FaContextOptionsBuilder builder, string authorizationstring)
        {
            var pair = builder.AuthorizationStrings.Single(o => o.Key == authorizationstring);

            var pairs = new Dictionary<string, FaContextOptions>();
            pairs.Add(pair.Key, pair.Value);

            var build = new FaContextOptionsBuilder();
            build.AuthorizationStrings = pairs;

            return build;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static FaContextOptions GetValue(this FaContextOptionsBuilder builder)
        {
            var key = builder.AuthorizationStrings.Single().Key;
            //
            builder.AuthorizationStrings.TryGetValue(key, out FaContextOptions options);
            return options;
        }
    }
}
