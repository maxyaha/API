using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace API.Extensions
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Max Project",
                    Description = "Max API Swagger surface",
                    Contact = new OpenApiContact { Name = "Max", Email = "contato@eduardopires.net.br", Url = new Uri("http://www.google.com") },
                    License = new OpenApiLicense { Name = "Max", Url = new Uri("http://www.google.com") }
                });

                //options.AddSecurityDefinition("Bearer", new ApiKeyScheme { Name = "ApiUserToken", In = "header", Type = "apiKey", Description = "JWT Authorization header using the Bearer scheme. Example: \"{token}\"" });
                //options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } } });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"{token}\"",
                    Name = "ApiUserToken",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(o => o.Endpoint(provider));
        }

        private static void Endpoint(this SwaggerUIOptions options, IApiVersionDescriptionProvider provider)
        {
            foreach (var description in provider.ApiVersionDescriptions.Where(o => !o.IsDeprecated))
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }

        internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
        {
            private readonly IApiVersionDescriptionProvider provider;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="provid"></param>
            public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provid)
            {
                this.provider = provid;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="options"></param>
            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in provider.ApiVersionDescriptions.Where(o => !o.IsDeprecated))
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo() { Title = $"Core API v{description.ApiVersion}", Version = description.ApiVersion.ToString() });
            }
        }
    }
}