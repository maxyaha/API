using System;
using System.Text;
using Microservice.Application.AppStart;
using Microservice.DataAccress.Events;
using Microservice.DataAccress.Features;
using Microservice.DataAccress.Tester;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shareds.DesignPatterns.IoC;

namespace Microservice.Application.AppStart
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //var appsettings = configuration.GetSection("AppSettings");

            //// Configure strongly typed settings objects       
            //services.Configure<AppSettings>(appsettings);

            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<TesterStoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EventConnection")));

         

            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EventConnection")));
        }
    }
}