using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Exceptions;

namespace Workflow.Nodes
{
    public class NodeService
    {
        public static Node? GetStartNode(Flow flow)
        {
            return flow.DrawFlow.FirstOrDefault().Value.Data.FirstOrDefault(item => item.Value.Inputs.Count == 0).Value;
        }
        public static Node? GetNextNode(Context context)
        {
            if (context.TryGet("NextNode", out Node? nextNode) && nextNode != null)
            {
                context.Delete<Node>("NextNode");
                return nextNode;
            }
            return null;
        }
        public static Node GetNext(Flow flow, OutputConnection outputConnection)
        {
            return flow.DrawFlow.First().Value.Data.First(item => item.Value.Id == Convert.ToInt32(outputConnection.Node)).Value;
        }
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