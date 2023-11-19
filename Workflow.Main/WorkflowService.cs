using System.Collections.Generic;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Nodes;
using Workflow.NodeSteps;

namespace Workflow.Main
{
    public class WorkflowService
    {
        private readonly NodeStepsService _nodeStepsService;
        public WorkflowService()
        {
            _nodeStepsService = new NodeStepsService();
        }

        public async Task<Context> RunAsync(Flow flow, Dictionary<string, string> input)
        {
            var context = new Context();
            context.Add("Flow", flow);
            foreach (var item in input)
            {
                context.Add(item.Key, item.Value);
            }

            return await ProcessNextAsync(flow, context);
        }
        private async Task<Context> ProcessNextAsync(Flow flow, Context context)
        {
            var node = NodeService.GetStartNode(flow);

            while (node != null)
            {
                context = await _nodeStepsService.GetNodeStep(node.Name).ProcessAsync(flow, node, context);
                node = NodeService.GetNextNode(context);
            }
            return context;
        }
    }
}
