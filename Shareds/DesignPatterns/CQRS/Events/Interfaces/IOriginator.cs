namespace Shareds.DesignPatterns.CQRS.Events.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOriginator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        BaseMemento GetMemento();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memento"></param>
        void SetMemento(BaseMemento memento);
    }
}
