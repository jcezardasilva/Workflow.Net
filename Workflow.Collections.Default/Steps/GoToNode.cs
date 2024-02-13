using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Flows;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.Collections.Default.Steps
{
    /// <summary>
    /// Redirects the workflow execution to a specific node
    /// </summary>
    public class GoToNode : INodeStepAsync
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
            var data = NodeService.GetData<GoToData>(node);

            var nodeId = NodeService.SetValues(data.NodeId,context);
            var pageName = NodeService.SetValues(data.PageName,context);

            var nextNode = NodeService.GetNode(flow.Pages[pageName],nodeId);
            if (nextNode != null)
            {
                context.Upsert("NextNode", nextNode);
            }

            return Task.FromResult(context);
        }
    }
}
