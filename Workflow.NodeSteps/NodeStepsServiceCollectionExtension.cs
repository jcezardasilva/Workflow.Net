using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Interfaces;

namespace Workflow.NodeSteps
{
    /// <summary>
    /// An IServiceCollection extension to add a NodeStepsService instance to the parent IServiceCollection
    /// </summary>
    public static class NodeStepsBuilderExtension
    {
        /// <summary>
        /// Adds a NodeStepsService to the parent IServiceCollection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static INodeStepsBuilder AddNodeSteps(this IServiceCollection services)
        {
            services.AddSingleton<INodeStepsService,NodeStepsService>();
            return new NodeStepsBuilder(services);
        }
    }
}
