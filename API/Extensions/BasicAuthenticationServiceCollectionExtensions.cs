using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shareds.Authentication.Basic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class BasicAuthenticationServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBasic(this IServiceCollection services)
        {
            // Configure basic authentication 
            services.AddAuthentication().AddBasic(BasicAuthenticationDefaults.AuthenticationScheme, null);

            // Configure DI for application services
            services.AddTransient<IConfigureOptions<AuthenticationOptions>, ConfigureAuthenticationOptions>();
            services.AddSingleton<IConfigureOptions<BasicAuthenticationOptions>, ConfigureBasicOptions>();

            return services;
        }

        /// <summary>
        /// Identity made Cookie authentication the default.
        /// However, we want Basic Auth to be the default.
        /// </summary>
        internal class ConfigureAuthenticationOptions : IConfigureOptions<AuthenticationOptions>
        {
            public ConfigureAuthenticationOptions()
            {
            }

            public void Configure(AuthenticationOptions options)
            {
                options.DefaultScheme = BasicAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = BasicAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = BasicAuthenticationDefaults.AuthenticationScheme;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal class ConfigureBasicOptions : IConfigureNamedOptions<BasicAuthenticationOptions>
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="options"></param>
            public void Configure(string name, BasicAuthenticationOptions options)
            {
                options.Realm = "idunno";
                options.Events = new BasicAuthenticationEvents();
                options.Events.OnValidateCredentials += HandleValidateCredentials;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="options"></param>
            public void Configure(BasicAuthenticationOptions options)
            {
                Configure(null, options);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            /// <returns></returns>
            private async Task HandleValidateCredentials(ValidateCredentialsContext context)
            {
                IEnumerable<Claim> claims = null;
                
                try
                {
                    // AUTHS 1FA|2FA

                    // SIGN-IN AUTHENTICATION
                    // USERNAME:PASSWORD :: AUTH BY THAI IDENTITY NUMBER & NULL|PHONE NUMBER
                    // TWO-FACTOR AUTHENTICATION
                    // USERNAME:PASSWORD :: AUTH BY PHONE NUMBER & NUMBER 6 DIGI

                    // PAYLOAD SET.
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
                finally
                {
                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                    context.Success();
                }
                //
                await Task.CompletedTask.ConfigureAwait(false);
            }
        }
    }
}
