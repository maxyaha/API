using Microservice.Companion.Controllers;
using Shareds.DesignPatterns.CQRS.Commands;

namespace API.AppStart
{
    /// <summary>
    /// 
    /// </summary>
    public class InitializeConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterInitializer()
        {
            //new PartyInitializer().InitializeObject(new CommandBus(new CommandHandlerFactory()));

           // new FeatureInitializer().InitializeObject(new CommandBus(new CommandHandlerFactory()));
        }
    }
}
