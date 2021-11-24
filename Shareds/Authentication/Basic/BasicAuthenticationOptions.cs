using Microsoft.AspNetCore.Authentication;
using System;

namespace Shareds.Authentication.Basic
{
    /// <summary>
    /// Contains the options used by the BasicAuthenticationMiddleware
    /// </summary>
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
        private string realm;

        /// <summary>
        /// Create an instance of the options initialized with the default values
        /// </summary>
        public BasicAuthenticationOptions()
        {
        }

        /// <summary>
        /// Gets or sets the Realm sent in the WWW-Authenticate header.
        /// </summary>
        /// <remarks>
        /// The realm value (case-sensitive), in combination with the canonical root URL
        /// of the server being accessed, defines the protection space.
        /// These realms allow the protected resources on a server to be partitioned into a
        /// set of protection spaces, each with its own authentication scheme and/or
        /// authorization database.
        /// </remarks>
        public string Realm { get { return realm; } set { realm = (!string.IsNullOrEmpty(value) && !IsAscii(value)) ? throw new ArgumentException("Realm must be US ASCII") : value; } }
        /// <summary>
        /// Gets or sets a flag indicating if the handler will prompt for authentication on HTTP requests.
        /// </summary>
        /// <remarks>
        /// If you set this to true you're a horrible person.
        /// </remarks>
        public bool AllowInsecureProtocol { get; set; }
        /// <summary>
        /// The object provided by the application to process events raised by the basic authentication middleware.
        /// The application may implement the interface fully, or it may create an instance of BasicAuthenticationEvents
        /// and assign delegates only to the events it wants to process.
        /// </summary>
        public new BasicAuthenticationEvents Events { get { return base.Events as BasicAuthenticationEvents; } set { base.Events = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsAscii(string realms)
        {
            foreach (char domain in realms)
                if (domain < 32 || domain >= 127)
                    return false;

            return true;
        }
    }
}
