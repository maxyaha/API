using System;
using Microservice.DataAccress.Events;
using Microservice.DataAccress.IPD;
using Microservice.DataAccress.Tester;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionstring = configuration.GetConnectionString("EventConnection");

            services.AddDbContext<TesterStoreContext>(o => o.UseSqlServer(connectionstring), ServiceLifetime.Transient);
            services.AddDbContext<EventStoreContext>(o => o.UseSqlServer(connectionstring), ServiceLifetime.Transient);
            services.AddDbContext<IpdStoreContext>(o => o.UseSqlServer(connectionstring), ServiceLifetime.Transient);

       
        }
    }
}