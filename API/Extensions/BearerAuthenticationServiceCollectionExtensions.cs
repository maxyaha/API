using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shareds.Authentication.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Net.Http;
using Shareds.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Shareds.Utilizing;

namespace API.Extensions
{
    public static class BearerAuthenticationServiceCollectionExtensions
    {
        internal static BearerAuthContext Context { get => new BearerAuthContext(SadServerFaContextOptionsExtensions.Builder.UseSadServer("BearerAuthorization").GetValue()); }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBearer(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, configureOptions: null);

            // CONFIGURE DI FOR APPLICATION SERVICES
            services.AddTransient<IConfigureOptions<AuthenticationOptions>, ConfigureAuthenticationOptions>();
            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            // 
            return services;
        }

        public static string BearerFormat(this JwtSecurityToken token, bool active = true)
        {
            return !active
                ? string.Format("{0}", new JwtSecurityTokenHandler().WriteToken(token))
                : string.Format("{0} {1}", JwtBearerDefaults.AuthenticationScheme, new JwtSecurityTokenHandler().WriteToken(token));
        }

      

        public static void BearerAuthenticationHeaders(this HttpClient http, FaContext context = null, IEnumerable<Claim> claims = null, string schema = AuthenticationDefaults.XAuthorizationHeaders, bool prefix = true)
        {
            var bearer = new BearerAuthContext();
            bearer.FillProperties(context ?? Context);
            // HTTP AUTHENTICATION HEADER SECURITY TOKEN
            var token = bearer.GetTokenSecurityParameters().BearerFormat(prefix);
            http.DefaultRequestHeaders.Add(schema, token);
        }


        /// <summary>
        /// Identity made Cookie authentication the default. However, we want JWT Bearer Auth to be the default.
        /// </summary>
        internal class ConfigureAuthenticationOptions : IConfigureOptions<AuthenticationOptions>
        {
            public ConfigureAuthenticationOptions(IConfiguration configuration)
            {
                SadServerFaContextOptionsExtensions.Configuration = configuration;
            }

            public void Configure(AuthenticationOptions options)
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        }
        /// <summary>
        ///  
        /// </summary>
        internal class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
        {
            public ConfigureJwtBearerOptions()
            {
            }
            /// <summary>
            /// Default case: no scheme name was specified
            /// </summary>
            /// <param name="options"></param>
            public void Configure(JwtBearerOptions options)
            {
                Configure(string.Empty, options);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="options"></param>
            public void Configure(string name, JwtBearerOptions options)
            {
                options.Events = new JwtBearerEvents();
                options.Events.OnMessageReceived += HandleMessageReceived;

                options.TokenValidationParameters = Context.GetTokenValidationParameters();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            /// <returns></returns>
            private static Task HandleMessageReceived(MessageReceivedContext context)
            {
                var header = context.HttpContext.Request.Headers[AuthenticationDefaults.XAuthorizationHeaders].ToString();

                if (!string.IsNullOrEmpty(header) && header.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    context.Token = header.Substring("Bearer ".Length).Trim();

                return Task.CompletedTask;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal class BearerAuthContext : FaContext
        {
            public BearerAuthContext()
            {
            }

            public BearerAuthContext(FaContextOptions options) : base(options)
            {
            }

            public override Dictionary<string, string> Claimation { get; set; }


            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public JwtSecurityToken GetTokenSecurityParameters(IEnumerable<Claim> payloads = null)
            {
                var claims = GetPayload(payloads);
                var issuer = GetIssuer();
                var audience = GetAudience();
                var secret = GetSigningKey();
                // EXPIRES DATE.
                var expires = GetExpires();
                // SIGNING CREDENTIALS.
                var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
                // CREATE THE JWT SECURITY TOKEN AND ENCODE IT.
                return new JwtSecurityToken(issuer: issuer, audience: audience, claims: claims, expires: expires, signingCredentials: credentials);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public TokenValidationParameters GetTokenValidationParameters()
            {
                var parameters = new TokenValidationParameters();
                // THE SIGNING KEY MUST MATCH!
                parameters.ValidateIssuerSigningKey = this.Validation.SigningKey;
                parameters.IssuerSigningKey = this.GetSigningKey();
                // VALIDATE THE JWT ISSUER (ISS) CLAIM  
                parameters.ValidateIssuer = this.Validation.Issuer;
                parameters.ValidIssuer = this.Validation.Server;
                // VALIDATE THE JWT AUDIENCE (AUD) CLAIM  
                parameters.ValidateAudience = this.Validation.Audience;
                parameters.ValidAudience = this.Validation.Permissions;
                // VALIDATE THE TOKEN EXPIRY  
                parameters.ValidateLifetime = this.Validation.Lifetime;
                // DELAY OF TOKEN WHEN EXPIRE
                parameters.ClockSkew = this.GetClockSkew();

                return parameters;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private SecurityKey GetSigningKey()
            {
                return new SymmetricSecurityKey(SingleByte());
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="issinglebyte"></param>
            /// <returns></returns>
            private byte[] SingleByte()
            {
                return Encoding.UTF8.GetBytes(this.Validation.Authbase);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private DateTime? GetExpires()
            {
                DateTime? expires = null;
                if (this.Validation.Lifetime)
                    expires = DateTime.Now.AddSeconds(this.Validation.Timeout);
                return expires;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private TimeSpan GetClockSkew()
            {
                return TimeSpan.FromSeconds(this.Validation.Delay);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private string GetIssuer()
            {
                return this.Validation.Issuer
                    ? this.Validation.Server
                    : null;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private string GetAudience()
            {
                return this.Validation.Audience
                    ? this.Validation.Permissions
                    : null;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            private IEnumerable<Claim> GetPayload(IEnumerable<Claim> payloads = null)
            {
                var claims = new List<Claim>();

                // CLAIM TYPES.
                foreach (var payload in payloads ?? new List<Claim>())
                    if (payload.Type == ClaimTypes.GivenName)
                        claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, payload.Value));
                // CLAIM TYPES. LOAGING PAYLOADs.
                foreach (var payload in payloads ?? new List<Claim>())
                    claims.Add(payload);
                // CLAIM TYPES, LOAGING CLAIMATIONs.
                foreach (var payload in this.Claimation)
                    claims.Add(new Claim(payload.Key, string.Format(payload.Value, DateTime.Now)));

                return claims;
            }
        }
    }
}
