using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Flows;
using Workflow.Domain.Exceptions;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.Collections.Default.Steps
{
    /// <summary>
    /// A Node to execute a Http Request.
    /// </summary>
    public class HttpRequestNode : INodeStepAsync
    {
        /// <summary>
        /// Executes the node
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="node"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{HttpRequestNode}"></exception>
        public async Task<Context> ProcessAsync(Flow flow, Node node, Context context)
        {
            var data = NodeService.GetData<HttpRequestData>(node);

            var method = NodeService.SetValues(data.Method.ToUpper(), context);
            var url = NodeService.SetValues(data.Url, context);

            var request = new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri(url)
            };
            var httpResponseMessage = await new HttpClient().SendAsync(request) ?? throw new WorkflowException<HttpRequestNode>("The http request failed.");

            context.Add(data.OutputStatus, (int)httpResponseMessage.StatusCode);
            context.Add(data.OutputContentType, httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty);
            context.Add(data.OutputContent, await httpResponseMessage.Content.ReadAsStringAsync());

            var outputKey = httpResponseMessage.IsSuccessStatusCode ? "output_1" : "output_2";

            NodeService.SetNext(flow, node, context, outputKey);
            return context;
        }
    }
}
