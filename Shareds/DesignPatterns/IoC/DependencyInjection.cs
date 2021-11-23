using StructureMap;
using System;

namespace Shareds.DesignPatterns.IoC
{
    public static class DependencyInjection
    {
        private static Container container;

        /// <summary>
        /// Registration for container.
        /// </summary>
        /// <param name="configuration"></param>
        public static void Register(Action<ConfigurationExpression> configuration)
        {
            container = new Container(configuration);
        }
        /// <summary>
        /// Gets the container for from StructureMap.
        /// </summary>
        public static Container Container { get { return container; } }
    }
}
