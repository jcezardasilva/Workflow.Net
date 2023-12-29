using System.Collections.Generic;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;

namespace Workflow.Main
{
    /// <summary>
    /// An extensible workflow engine
    /// </summary>
    public class WorkflowService
    {
        private readonly INodeStepsService _nodeStepsService;
        /// <summary>
        /// Initializes the service with an INodeStepsService instance
        /// </summary>
        /// <param name="nodeStepsService"></param>
        public WorkflowService(INodeStepsService nodeStepsService)
        {
            _nodeStepsService = nodeStepsService;
        }
        /// <summary>
        /// Executes the received flow
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public async Task<Context> RunAsync(Flow flow)
        {
            var context = new Context();
            context.Add("Flow", flow);

            return await ProcessNextAsync(flow, context);
        }
        /// <summary>
        /// Executes the received flow adding the input dictionary to the data context.
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Executes the received flow and context
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<Context> RunAsync(Flow flow, Context context)
        {
            context.Upsert("Flow", flow);

            return await ProcessNextAsync(flow, context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="context"></param>
        /// <returns></returns>
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
