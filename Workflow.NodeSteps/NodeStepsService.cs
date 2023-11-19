using Microsoft.Extensions.DependencyInjection;
using Workflow.Domain.Exceptions;
using Workflow.Domain.Interfaces;
using Workflow.NodeSteps.Steps;

namespace Workflow.NodeSteps
{
    public class NodeStepsService
    {
        private readonly ServiceProvider _serviceProvider;
        /// <summary>
        /// Initializes the factore
        /// </summary>
        public NodeStepsService()
        {
            _serviceProvider = new ServiceCollection()
                .AddTransient<INodeStepAsync, ConsoleNode>()
                .AddTransient<INodeStepAsync, HttpRequestNode>()
                .AddTransient<INodeStepAsync, CSharpScriptNode>()
                .BuildServiceProvider();
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