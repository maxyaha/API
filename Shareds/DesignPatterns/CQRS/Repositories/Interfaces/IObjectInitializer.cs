using Shareds.DesignPatterns.CQRS.Commands;

namespace Shareds.DesignPatterns.CQRS.Repositories.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandBus"></typeparam>
    public interface IObjectInitializer<in TCommandBus> where TCommandBus : CommandBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void InitializeObject(TCommandBus command);
    }
}
