using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.NodeSteps.Steps
{
    public class ConsoleNode : INodeStepAsync
    {
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
