using Microsoft.AspNetCore.Authentication;
using System;

namespace Shareds.Authentication.Basic
{
    /// <summary>
    /// Extension methods to add Basic authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class BasicAuthenticationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder) 
            => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="scheme"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string scheme)
            => builder.AddBasic(scheme, options: null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, Action<BasicAuthenticationOptions> options)
            => builder.AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="scheme"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string scheme, Action<BasicAuthenticationOptions> options)
            => builder.AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(scheme, options);
    }
}
