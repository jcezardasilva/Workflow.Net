using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.Collections.Default.Steps
{
    /// <summary>
    /// A Node to print data to the system console
    /// </summary>
    public class ConsoleNode : INodeStepAsync
    {
        /// <summary>
        /// Executes the node.
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="node"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<Context> ProcessAsync(Flow flow, Node node, Context context)
        {
            var data = NodeService.GetData<ConsoleData>(node);

            var text = NodeService.SetValues(data.Message,context);

            Console.WriteLine(text);

            NodeService.SetNext(flow, node, context, "output_1");

            return Task.FromResult(context);
        }
    }
}
