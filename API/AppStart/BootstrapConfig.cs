using Microservice.BusinessLogic;
using Microservice.Companion.Controllers.CommandHandlers.Tester;
using Microservice.DataAccress.Events;
using Microservice.DataAccress.Features;
using Microservice.DataAccress.Features.Repositories;
using Microservice.DataAccress.IPD;
using Microservice.DataAccress.Tester;
using Microservice.DataAccress.Tester.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.CQRS.Events;
using Shareds.DesignPatterns.CQRS.Events.Interfaces;
using Shareds.DesignPatterns.CQRS.Repositories;
using Shareds.DesignPatterns.CQRS.Repositories.Interfaces;
using Shareds.DesignPatterns.IoC;
using Shareds.DesignPatterns.Repository.DatabaseContext;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;
using Shareds.Logging;
using Shareds.Logging.Interfaces;
using StructureMap;

namespace Microservice.Application.AppStart
{
    public static class BootstrapConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterBootstraps(this IServiceCollection services)
        {
            //services.AddScoped<IEventStoreContext, EventStoreContext>();

            //// Application
            //services.AddScoped<IEventManager, EventManager>();
            //services.AddScoped<IEventBus, EventBus>();
            //services.AddScoped<IEventHandlerFactory, EventHandlerFactory>();
            //services.AddScoped<ICommandBus, CommandBus>();
            //services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
          
            //services.AddScoped<ITestRepositoryAsync, TestRepositoryAsync>();
            //services.AddScoped<ITestManager, TestManager>();
            //services.AddScoped<ILogger, Logger>();
            //services.AddScoped<ITesterStoreContext, TesterStoreContext>();

 
            DependencyInjection.Register(register =>
            {
                // Register IoC data access.
                register.For(typeof(IContext<>)).Use(typeof(Context<>));
                register.For(typeof(IRepository<>)).Use(typeof(Repository<>));

                // Register IoC event store connection.
                register.For<IEventStoreContext>().Use(() => new EventStoreContext());
                register.For<IEventManager>().Use<EventManager>();
                register.For<IEventBus>().Use<EventBus>();
                register.For<IEventHandlerFactory>().Use<EventHandlerFactory>();
                register.For<ICommandBus>().Use<CommandBus>();
                register.For<ICommandHandlerFactory>().Use<CommandHandlerFactory>();

                //// Register IoC micro store connection.
                register.For<ITesterStoreContext>().Use(() => new TesterStoreContext());
                register.For<ITestManager>().Use<TestManager>();

                register.For<IIpdStoreContext>().Use(() => new IpdStoreContext());
                // Register IoC micro store connection.
                //   register.For<IFeatureStoreContext>().Use(() => new FeatureStoreContext());


                // Register IoC log text file.
                register.For<ILogger>().Use<Logger>();


                // Scan all in repository.
                register.Scan(scanner =>
                {
                    #region Parties
                
                    #endregion Parties

                    #region Features
           
                    #endregion Features

                    #region Tester
                    scanner.AssemblyContainingType(typeof(TestRepositoryAsync));

                    #endregion Tester

                    scanner.WithDefaultConventions();
                });

                // Scan all in broadcast.
                register.Scan(scanner =>
                {
                    #region Parties
                    #endregion Parties

                    #region Tester
                    scanner.AssemblyContainingType<TestCommandHandler>();
                    #endregion Tester

                    scanner.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(IEventHandler<>));
                });

                register.Populate(services);
            });
        }
    }
}
