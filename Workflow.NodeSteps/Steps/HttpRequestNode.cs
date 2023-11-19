using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Exceptions;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.NodeSteps.Steps
{
    public class HttpRequestNode : INodeStepAsync
    {
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

            #region output
            context.Add(data.OutputStatus, (int)httpResponseMessage.StatusCode);
            context.Add(data.OutputContentType, httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty);
            context.Add(data.OutputContent, await httpResponseMessage.Content.ReadAsStringAsync());

            var outputKey = httpResponseMessage.IsSuccessStatusCode ? "output_1" : "output_2";

            NodeService.SetNext(flow, node, context, outputKey);
            return context;
            #endregion
        }
    }
}
