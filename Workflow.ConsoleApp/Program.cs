using dotenv.net;
using System.Text.Json;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Main;

namespace Workflow.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var jsonFlow = "{\"drawflow\":{\"Home\":{\"data\":{\"1\":{\"id\":1,\"name\":\"HttpRequestNode\",\"data\":{\"url\":\"https://dog-api.kinduff.com/api/facts\",\"method\":\"GET\",\"outputStatus\":\"http_status\",\"outputContentType\":\"http_contenttype\",\"outputContent\":\"http_content\"},\"class\":\"HttpRequestNode\",\"html\":\"HttpRequestNode\",\"typenode\":\"vue\",\"inputs\":{},\"outputs\":{\"output_1\":{\"connections\":[{\"node\":\"2\",\"output\":\"input_1\"}]}},\"pos_x\":137,\"pos_y\":89},\"2\":{\"id\":2,\"name\":\"ConsoleNode\",\"data\":{\"message\":\"Dog facts: {{http_content}}.\"},\"class\":\"ConsoleNode\",\"html\":\"ConsoleNode\",\"typenode\":\"vue\",\"inputs\":{\"input_1\":{\"connections\":[{\"node\":\"1\",\"input\":\"output_1\"}]}},\"outputs\":{\"output_1\":{\"connections\":[]},\"output_2\":{\"connections\":[]}},\"pos_x\":625,\"pos_y\":94}}}}}";
            var flow = JsonSerializer.Deserialize<Flow>(jsonFlow,new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? throw new Exception("Cannot parse the flow.");
            var input = new Dictionary<string, string>();
            var env = DotEnv.Read();

            foreach (var item in env)
            {
                input.Add(item.Key, item.Value);
            }

            foreach (var arg in args)
            {
                if (arg.Contains('='))
                {
                    var item = arg.Split('=');
                    input.Add(item[0], item[1]);
                }
            }

            var workflow = new WorkflowService();
            await workflow.RunAsync(flow, input);
        }
    }
}