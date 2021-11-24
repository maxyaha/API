using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Shareds.Authentication.Basic
{
    public class ValidateCredentialsContext : ResultContext<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Creates a new instance of <see cref="ValidateCredentialsContext"/>.
        /// </summary>
        /// <param name="context">The HttpContext the validate context applies too.</param>
        /// <param name="scheme">The scheme used when the Basic Authentication handler was registered.</param>
        /// <param name="options">The <see cref="BasicAuthenticationOptions"/> for the instance of
        /// <see cref="BasicAuthenticationMiddleware"/> creating this instance.</param>
        /// <param name="ticket">Contains the intial values for the identit.</param>
        public ValidateCredentialsContext(HttpContext context, AuthenticationScheme scheme, BasicAuthenticationOptions options) : base(context, scheme, options)
        {
        }

        /// <summary>
        /// The user name to validate.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to validate.
        /// </summary>
        public string Password { get; set; }
    }
}
