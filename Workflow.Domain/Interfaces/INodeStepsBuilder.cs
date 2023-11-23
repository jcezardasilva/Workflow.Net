using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Domain.Interfaces
{
    /// <summary>
    /// The NodeStepsService builder interface
    /// </summary>
    public interface INodeStepsBuilder
    {
        /// <summary>
        /// Adds a collection to the builder
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        INodeStepsBuilder AddCollection(IServiceCollection collection);
        /// <summary>
        /// Builds the NodeStepsService
        /// </summary>
        /// <returns></returns>
        INodeStepsService Build();
    }
}