using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Workflow.Collections.Default;
using Workflow.Domain.Entities.Flows;
using Workflow.Main;
using Workflow.NodeSteps;

namespace Workflow.Tests
{
    public class WorkflowTest
    {
        [Test]
        public async Task Should_Run_A_Flow()
        {
            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);

            var steps = new NodeStepsBuilder()
                .AddDefaultCollection()
                .Build();

            var output = await new WorkflowService(steps).RunAsync(flow);
            Assert.That(output, Is.Not.Null);
        }
    }
}
