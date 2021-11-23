using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Shareds.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class NamedLocker
    {
        private readonly ConcurrentDictionary<string, object> pairs = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// get a lock for use with a lock(){} block
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetLock(string name)
        {
            return this.pairs.GetOrAdd(name, s => new object());
        }
        /// <summary>
        /// run a short lock inline using a lambda
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="name"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public TResult RunWithLock<TResult>(string name, Func<TResult> body)
        {
            lock (this.pairs.GetOrAdd(name, s => new object()))
            {
                return body();
            }
        }
        /// <summary>
        /// run a short lock inline using a lambda
        /// </summary>
        /// <param name="name"></param>
        /// <param name="body"></param>
        public void RunWithLock(string name, Action body)
        {
            lock (this.pairs.GetOrAdd(name, s => new object()))
            {
                body();
            }
        }
        /// <summary>
        /// remove an old lock object that is no longer needed
        /// </summary>
        /// <param name="name"></param>
        public void RemoveLock(string name)
        {
            object o;
            this.pairs.TryRemove(name, out o);
        }
    }
}
