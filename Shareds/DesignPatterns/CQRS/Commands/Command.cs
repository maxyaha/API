using Shareds.DesignPatterns.CQRS.Commands.Interfaces;
using System;

namespace Shareds.DesignPatterns.CQRS.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum Commandstates
    {
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        Added = 1,
        /// <summary>
        /// 
        /// </summary>
        Changed = 2,
        /// <summary>
        /// 
        /// </summary>
        Removed = 3,
    }
    /// <summary>
    /// 
    /// </summary>
    public class Command : ICommand
    {
        public Command(Guid id, int version, Commandstates state)
        {
            ID = id;
            Version = version;
            State = state;
        }

        /// <summary>
        /// 
        /// </summary>
        public Commandstates State { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid ID { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int Version { get; private set; }
    }
}
