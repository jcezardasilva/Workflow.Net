using System.Net.Http;
using System;
using System.Threading.Tasks;
using WorkflowNet.Collections.Example.Models;
using WorkflowNet.Core.Extensions.Dictionary;
using WorkflowNet.Core.Interfaces;
using WorkflowNet.Core.Exceptions;
using System.Linq;

namespace WorkflowNet.Collections.Example.ActionHandlers
{
    public class HttpGetActionHandler : IActionHandler
    {
        public async Task<IContext> ProcessAsync(IContext context)
        {
            var action = context.GetCurrentAction();
            var model = action.Variables.ToModel<HttpGetActionModel>();

            var request = new HttpRequestMessage()
            {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri(model.RequestUri)
            };
            var httpResponseMessage = await new HttpClient().SendAsync(request) ?? throw new WorkflowException<HttpGetActionHandler>("The http request failed.");

            context.Add(model.ResponseStatus, httpResponseMessage.StatusCode);
            context.Add(model.ResponseContent, httpResponseMessage.Content.ReadAsStringAsync());
            context.Add(model.ResponseContentType, httpResponseMessage.Content.Headers.ContentType?.MediaType ?? string.Empty);

            var output = httpResponseMessage.IsSuccessStatusCode ? action.Outputs.ElementAt(0) : action.Outputs.ElementAt(1);
            context.SetOutputConnector(output);
            return context;
        }
    }
}
