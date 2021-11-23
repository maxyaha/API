using System;

using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Application.AppStart
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            BootstrapConfig.RegisterBootstraps(services);
        }
    }
}