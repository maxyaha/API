using Microservice.BusinessLogic;
using Microservice.Companion.Controllers.CommandHandlers.Tester;
using Microservice.DataAccress.Events;
using Microservice.DataAccress.Accounts;
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

namespace API.Extensions
{
    public static class StructureMapServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static IServiceCollection RegisterBootstraps(this IServiceCollection services)
        {
            
 
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

                register.For<IAccountStoreContext>().Use(() => new AccountStoreContext());
                register.For<IAccountManager>().Use<AccountManager>();

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
            return services;

        }
    }
}
