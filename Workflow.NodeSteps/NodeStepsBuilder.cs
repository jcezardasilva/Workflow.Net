using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Interfaces;

namespace Workflow.NodeSteps
{
    /// <summary>
    /// Creates NodeStepsService instances
    /// </summary>
    public class NodeStepsBuilder : INodeStepsBuilder
    {
        private readonly IServiceCollection _serviceCollection;
        /// <summary>
        /// Initializes the builder with an empty ServiceCollection
        /// </summary>
        public NodeStepsBuilder()
        {
            _serviceCollection = new ServiceCollection();
        }
        /// <summary>
        /// Initializes the builder with a ServiceCollection
        /// </summary>
        /// <param name="collection"></param>
        public NodeStepsBuilder(IServiceCollection collection)
        {
            _serviceCollection = collection;
        }
        /// <summary>
        /// Adds a collection to the collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public INodeStepsBuilder AddCollection(IServiceCollection collection)
        {
            foreach (var item in collection.Where(item => typeof(INodeStepAsync).IsAssignableFrom(item.ServiceType)).ToList())
            {
                _serviceCollection.Add(item);
            }
            return this;
        }
        /// <summary>
        /// Builds the NodeStepsService with a ServiceProvider
        /// </summary>
        /// <returns></returns>
        public INodeStepsService Build()
        {
            return new NodeStepsService(_serviceCollection.BuildServiceProvider());
        }
    }
}
