using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using System;
using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory command;

        public CommandBus(ICommandHandlerFactory command)
        {
            this.command = command;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Send<T>(T command) where T : Command
        {
            var handler = this.command.GetHandler<T>();

            try
            {
                await handler.Execute(command).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                throw new UnregisteredDomainCommandException("no handler registered");
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class UnregisteredDomainCommandException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public UnregisteredDomainCommandException(string message) : base(message) { }
    }
}
