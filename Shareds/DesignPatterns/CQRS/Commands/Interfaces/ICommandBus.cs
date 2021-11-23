using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Commands.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        Task Send<T>(T command) where T : Command;
    }
}
