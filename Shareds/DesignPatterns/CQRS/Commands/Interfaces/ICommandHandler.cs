using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Commands.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        Task ExecuteAsync(TCommand handle);
    }
}
