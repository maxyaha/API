using System;

using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            StructureMapServiceCollectionExtensions.RegisterBootstraps(services);
        }
    }
}