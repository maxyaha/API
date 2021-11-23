namespace Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContext<out T>
    {
        /// <summary>
        /// Gets the context of the database.
        /// </summary>
        T DatabaseContext { get; }
    }
}
