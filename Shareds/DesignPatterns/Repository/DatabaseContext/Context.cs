using Shareds.DesignPatterns.IoC;
using Shareds.DesignPatterns.Repository.DatabaseContext.Interfaces;

namespace Shareds.DesignPatterns.Repository.DatabaseContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Context<T> : IContext<T>
    {
        /// <summary>
        ///  Gets the context of the database.
        /// </summary>
        public T DatabaseContext { get { return DependencyInjection.Container.GetInstance<T>(); } }
    }
}
