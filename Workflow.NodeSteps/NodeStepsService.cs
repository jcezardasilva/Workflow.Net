using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Exceptions;
using Workflow.Domain.Interfaces;

namespace Workflow.NodeSteps
{
    /// <summary>
    /// Provides a steps container to the workflow
    /// </summary>
    public class NodeStepsService : INodeStepsService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes the factore
        /// </summary>
        public NodeStepsService(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }
        /// <summary>
        /// Gets a NodeStep instance
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{NodeStepService}"></exception>
        public INodeStepAsync GetNodeStep(string nodeName)
        {
            return _serviceProvider.GetServices<INodeStepAsync>().FirstOrDefault(s => s.GetType().Name == nodeName) ??
                throw new WorkflowException<NodeStepsService>($"NodeStep {nodeName} not found.");
        }
    }
}