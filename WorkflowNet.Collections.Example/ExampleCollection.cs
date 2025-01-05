using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Collections.Example
{
    /// <summary>
    /// The standard Workflow Nodes collection
    /// </summary>
    public static class ExampleCollection
    {
        /// <summary>
        /// Add the collection to the IServiceCollection
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddExampleCollection(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = typeof(IActionHandler);
            var actionHandlers = assembly.GetTypes().Where(p => type.IsAssignableFrom(p));

            foreach (var handler in actionHandlers)
            {
                services.AddSingleton(typeof(IActionHandler), handler);
            }
            return services;
        }
    }
}
