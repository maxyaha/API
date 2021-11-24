using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

namespace Shareds.Authentication.Basic
{
    public class BasicAuthenticationFailedContext : ResultContext<BasicAuthenticationOptions>
    {
        public BasicAuthenticationFailedContext(HttpContext context, AuthenticationScheme scheme, BasicAuthenticationOptions options) : base(context, scheme, options)
        {
        }

        public Exception Exception { get; set; }
    }
}
