using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;


namespace API.Extensions
{
    
    using Test = Controllers.Tester;

    public static class ApiVersionServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                // HTTP Header-Based Versioning
                // Report api version (api-supported-versions or api-deprecated-versions)
                o.ReportApiVersions = true;
                // Default api version
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                // If you do not wish to change the URL of the API then you can send the version of API in the HTTP header.
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                // If you have lots of versions of the API, instead of putting the ApiVersion attribute on all the controllers, we can assign the versions using a conventions property.
                // There might be a case when we need to opt out the version for some specific APIs.
                // In such cases, we can opt out of this API by adding the attribute [ApiVersionNeutral].
              
                o.Conventions.Controller<Test.v1.TestController>().HasApiVersion(new ApiVersion(1, 0));

            });

            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
