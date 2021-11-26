using System;
using API.AppStart;
using API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shareds.DesignPatterns.IoC;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var appsettings = Configuration.GetSection("AppSettings"); 
            services.Configure<AppSettings>(appsettings);

            services.AddControllers();
            services.AddCors();
            services.AddDatabaseConfiguration(Configuration);
            services.AddBasic();
            services.AddBearer();
            services.AddApiVersion();
            services.AddSwaggerConfiguration();
            services.AddDependencyInjectionConfiguration();

            return DependencyInjection.Container.GetInstance<IServiceProvider>();
   
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();     
            app.UseSwaggerSetup();
            app.UseAuthentication();
           // app.UseAuthorization();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
           
        }
    }
}
