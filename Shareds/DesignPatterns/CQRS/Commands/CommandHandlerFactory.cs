using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using Shareds.DesignPatterns.IoC;

namespace Shareds.DesignPatterns.CQRS.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            return DependencyInjection.Container.GetInstance<ICommandHandler<T>>();
        }
    }
}
