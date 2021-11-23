namespace Shareds.DesignPatterns.CQRS.Commands.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ICommandHandler<T> GetHandler<T>() where T : Command;
    }
}
