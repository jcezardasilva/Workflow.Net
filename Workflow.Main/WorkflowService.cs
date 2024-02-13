using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Flows;
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
        /// Executes nodes recursively until find no more nodes.
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<Context> ProcessNextAsync(Flow flow, Context context)
        {
            var nodes = new List<Node>();
            var node = NodeService.GetStartNode(flow);
            if(node != null)
            {
                nodes.Add(node);
            }

            while (nodes.Count>0)
            {
                var tasks = new List<Task<Context>>();
                tasks.AddRange(nodes.Select(node=> _nodeStepsService.GetNodeStep(node.Name).ProcessAsync(flow, node, context)));
                
                var contexts = await Task.WhenAll(tasks);

                foreach(var item in contexts)
                {
                    context.AddRange(item.ToDictionary());
                }
                nodes = NodeService.GetNextNodes(context)?.ToList() ?? new List<Node>();
            }
            return context;
        }
    }
}
