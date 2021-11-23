using System.Threading.Tasks;

namespace Shareds.DesignPatterns.CQRS.Events.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        Task Execute(TEvent handle);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IHandle<TEvent> where TEvent : Event
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        void Handle(TEvent handle);
    }
}
