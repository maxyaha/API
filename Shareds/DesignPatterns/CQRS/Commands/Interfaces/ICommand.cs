using System;

namespace Shareds.DesignPatterns.CQRS.Commands.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ID { get; }
    }
}
