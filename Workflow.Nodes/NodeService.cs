using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Flows;
using Workflow.Domain.Exceptions;

namespace Workflow.Nodes
{
    /// <summary>
    /// Provides features to the workflow Node Steps
    /// </summary>
    public class NodeService
    {
        /// <summary>
        /// Retrieves the flow startup node
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public static Node? GetStartNode(Flow flow)
        {
            var startPage = !string.IsNullOrEmpty(flow.MainPage) ? flow.Pages[flow.MainPage] : flow.Pages.FirstOrDefault().Value;
            return GetStartNode(startPage);
        }
        /// <summary>
        /// Retrieves the page startup node
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Node? GetStartNode(FlowPage page)
        {
            if (!string.IsNullOrEmpty(page.StartNodeId))
            {
                return page.Data[page.StartNodeId];
            }
            return null;
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
                context.Delete("NextNode");
                return nextNode;
            }
            return null;
        }
        /// <summary>
        /// Retrieves the next flow node group. If it not exists returns null.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IEnumerable<Node>? GetNextNodes(Context context)
        {
            if (context.TryGet("NextNodes", out List<Node>? nextNodes) && nextNodes != null)
            {
                context.Delete("NextNodes");
                return nextNodes;
            }
            var node = GetNextNode(context);
            if(node != null)
            {
                return new List<Node>() { node };
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
            return flow.Pages.First().Value.Data.First(item => item.Value.Id == Convert.ToInt32(outputConnection.Node)).Value;
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
        /// Sets the next node to the data context. Supports parallel multiple nodes.
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="node"></param>
        /// <param name="context"></param>
        /// <param name="outputKey"></param>
        /// <param name="allowMultipleOutputs"></param>
        /// <returns></returns>
        public static Context SetNext(Flow flow, Node node, Context context, string outputKey, bool allowMultipleOutputs=false)
        {
            if (node.Outputs.Count > 0)
            {
                var output = node.Outputs[outputKey];
                if (output != null && output.Connections.Count > 0)
                {
                    if(allowMultipleOutputs)
                    {
                        var nodes = new List<Node>();
                        foreach(var connection in output.Connections)
                        {
                            nodes.Add(GetNext(flow, connection));
                        }
                        context.Upsert("NextNodes", nodes);
                    }
                    else
                    {
                        context.Upsert("NextNode", GetNext(flow, output.Connections.First()));
                    }
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
        /// Retrieves the node data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{NodeService}"></exception>
        public static bool TryGetData<T>(Node node, out T? value)
        {
            if(node.Data.TryGetValue(typeof(T).Name, out object? data) && data != null && data is T t)
            {
                value = t;
                return true;
            }
            value = default;
            return false;
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
                context.TryGet(match.Groups[1].Value, out string? value);
                text = text.Replace(match.Value, value ?? string.Empty);
            }
            return text;
        }
    }
}