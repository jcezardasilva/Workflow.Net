using Microsoft.Extensions.DependencyInjection;
using Workflow.Collections.Default.Steps;
using Workflow.Domain.Interfaces;

namespace Workflow.Collections.Default
{
    /// <summary>
    /// The standard Workflow Nodes collection
    /// </summary>
    public static class DefaultCollection
    {
        /// <summary>
        /// Retrieves an IServiceCollection with all the collection nodes.
        /// </summary>
        public static IServiceCollection Collection
        {
            get
            {
                return new ServiceCollection()
                    .AddSingleton<INodeStepAsync, ConsoleNode>()
                    .AddSingleton<INodeStepAsync, HttpRequestNode>()
                    .AddSingleton<INodeStepAsync, CSharpScriptNode>();
            }
        }
        /// <summary>
        /// A NodeStepsBuilder extension method to adds the collection to the parent IServiceCollection
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static INodeStepsBuilder AddDefaultCollection(this INodeStepsBuilder builder)
        {
            return builder.AddCollection(Collection);
        }
    }
}
