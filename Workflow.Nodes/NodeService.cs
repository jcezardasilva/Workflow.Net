using System.Text.Json;
using System.Text.RegularExpressions;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Exceptions;

namespace Workflow.Nodes
{
    /// <summary>
    /// Provides features to the workflow Node Steps
    /// </summary>
    public class NodeService
    {
        /// <summary>
        /// Retrieves the first flow node
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public static Node? GetStartNode(Flow flow)
        {
            return flow.DrawFlow.FirstOrDefault().Value.Data.FirstOrDefault(item => item.Value.Inputs.Count == 0).Value;
        }
        /// <summary>
        /// Retrieves the next flow node. If it not exists returns null.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Node? GetNextNode(Context context)
        {
            if (context.TryGet("NextNode", out Node? nextNode) && nextNode != null)
            {
                context.Delete<Node>("NextNode");
                return nextNode;
            }
            return null;
        }
        /// <summary>
        /// Retrieves the next node from flow definition
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="outputConnection"></param>
        /// <returns></returns>
        public static Node GetNext(Flow flow, OutputConnection outputConnection)
        {
            return flow.DrawFlow.First().Value.Data.First(item => item.Value.Id == Convert.ToInt32(outputConnection.Node)).Value;
        }
        /// <summary>
        /// Sets the next node to the data context.
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="node"></param>
        /// <param name="context"></param>
        /// <param name="outputKey"></param>
        /// <returns></returns>
        public static Context SetNext(Flow flow, Node node, Context context, string outputKey)
        {
            if (node.Outputs.Count > 0)
            {
                var output = node.Outputs[outputKey];
                if (output != null && output.Connections.Count > 0)
                {
                    context.Upsert("NextNode", GetNext(flow, output.Connections.First()));
                }
            }
            return context;
        }
        /// <summary>
        /// Retrieves the node data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{NodeService}"></exception>
        public static T GetData<T>(Node node)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(node.Data, options), options) ??
                throw new WorkflowException<NodeService>("Cannot parse the Data.");
        }
        /// <summary>
        /// Sets values from data context to the node variables tagged as '{{variable-name}}'.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string SetValues(string text, Context context)
        {
            string pattern = "\\{\\{([^}]+)\\}\\}";
            var regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(text);
            foreach (Match match in matches.ToList())
            {
                text = text.Replace(match.Value, context.Get<string>(match.Groups[1].Value));
            }
            return text;
        }
    }
}