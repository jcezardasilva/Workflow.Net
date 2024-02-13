using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Workflow.Domain.Entities.Flows;
using Workflow.Domain.Interfaces;
using Workflow.Main;

namespace Workflow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowsController : ControllerBase
    {
        private readonly INodeStepsService _nodeStepsService;
        public FlowsController(INodeStepsService nodeStepsService)
        {
            _nodeStepsService = nodeStepsService;
        }
        [HttpPost("Demo")]
        public async Task<IActionResult> RunDemoAsync([FromBody] Dictionary<string, string> input)
        {
            var jsonFlow = "{\"drawflow\":{\"Home\":{\"data\":{\"1\":{\"id\":1,\"name\":\"HttpRequestNode\",\"data\":{\"url\":\"https://dog-api.kinduff.com/api/facts\",\"method\":\"GET\",\"outputStatus\":\"http_status\",\"outputContentType\":\"http_contenttype\",\"outputContent\":\"http_content\"},\"class\":\"HttpRequestNode\",\"html\":\"HttpRequestNode\",\"typenode\":\"vue\",\"inputs\":{},\"outputs\":{\"output_1\":{\"connections\":[{\"node\":\"2\",\"output\":\"input_1\"}]}},\"pos_x\":137,\"pos_y\":89},\"2\":{\"id\":2,\"name\":\"ConsoleNode\",\"data\":{\"message\":\"Dog facts: {{http_content}}.\"},\"class\":\"ConsoleNode\",\"html\":\"ConsoleNode\",\"typenode\":\"vue\",\"inputs\":{\"input_1\":{\"connections\":[{\"node\":\"1\",\"input\":\"output_1\"}]}},\"outputs\":{\"output_1\":{\"connections\":[]},\"output_2\":{\"connections\":[]}},\"pos_x\":625,\"pos_y\":94}}}}}";
            var flow = JsonSerializer.Deserialize<Flow>(jsonFlow, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? throw new Exception("Cannot parse the flow.");

            var workflow = new WorkflowService(_nodeStepsService);
            var context = await workflow.RunAsync(flow, input);
            return Ok(context.Get<string>("http_content"));
        }
        [HttpPost("Run")]
        public async Task<IActionResult> RunAsync([FromBody] Flow flow)
        {
            var workflow = new WorkflowService(_nodeStepsService);
            var context = await workflow.RunAsync(flow);
            return Ok(context.Get<string>("http_content"));
        }
    }
}
