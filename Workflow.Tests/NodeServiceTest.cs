using System.IO;
using System.Text.Json;
using Workflow.Collections.Default.Steps;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.Flows;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.Tests
{
    public class NodeServiceTest
    {

        [Test]
        public void Should_Get_StartNode()
        {
            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);

            var node = NodeService.GetStartNode(flow);
            Assert.That(node, Is.Not.Null);
            Assert.That(node.Id, Is.EqualTo(1));
        }

        [Test]
        public void Should_Get_Null_For_NotDefined_StartNodeId()
        {
            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);

            flow.Pages.First().Value.StartNodeId = string.Empty;

            var node = NodeService.GetStartNode(flow);
            Assert.That(node, Is.Null);
        }

        [Test]
        public void Should_Get_StartNode_For_NotDefined_MainPage()
        {
            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);

            flow.MainPage = string.Empty;

            var node = NodeService.GetStartNode(flow);
            Assert.That(node, Is.Not.Null);
            Assert.That(node.Id, Is.EqualTo(1));
        }

        [Test]
        public void Should_Get_Node_Data()
        {
            var node = new Node()
            {
                Id = 1,
                Data = new Dictionary<string, object>()
                {
                    { "Message", "example message" }
                }
            };

            var data = NodeService.GetData<ConsoleData>(node);
            Assert.That(data,Is.Not.Null);
            Assert.That(data.Message, Is.Not.Empty);
            Assert.That(data.Message, Is.EqualTo("example message"));
        }

        [Test]
        public void Should_Get_Node_Data_Value()
        {
            var node = new Node()
            {
                Id = 1,
                Data = new Dictionary<string, object>()
                {
                    { "Message", "example message" }
                }
            };

            NodeService.TryGetData(node, "Message", out string? message);
            Assert.That(message, Is.Not.Null);
            Assert.That(message, Is.Not.Empty);
            Assert.That(message , Is.EqualTo("example message"));
        }

        [Test]
        public void Should_Set_Node_Data_Values()
        {
            string testMessage = "{{text}}";
            var context = new Context();
            context.Add("text", "example message");

            var text = NodeService.SetValues(testMessage, context);
            Assert.That(text, Is.Not.Null);
            Assert.That(text, Is.Not.Empty);
            Assert.That(text, Is.EqualTo("example message"));
        }

        [Test]
        public void Should_Bypass_SetValues()
        {
            string testMessage = "text";
            var context = new Context();
            context.Add("text", "example message");

            var text = NodeService.SetValues(testMessage, context);
            Assert.That(text, Is.Not.Null);
            Assert.That(text, Is.Not.Empty);
            Assert.That(text, Is.EqualTo("text"));
        }

        [Test]
        public void Should_Set_NextNode()
        {
            var context = new Context();
            context.Add("text", "example message");

            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);
            var node = NodeService.GetStartNode(flow);

            Assert.That(context.ContainsKey("NextNode"), Is.False);
            NodeService.SetNext(flow, node, context, "output_1");
            Assert.That(context.ContainsKey("NextNode"), Is.True);
        }

        [Test]
        public void Should_Set_NextNode_With_AllowMultiple()
        {
            var context = new Context();
            context.Add("text", "example message");

            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);
            var node = NodeService.GetStartNode(flow);

            Assert.That(context.ContainsKey("NextNodes"), Is.False);
            NodeService.SetNext(flow, node, context, "output_1",true);
            Assert.That(context.ContainsKey("NextNodes"), Is.True);
            var nextNodes = context.Get<List<Node>>("NextNodes");
            Assert.That(nextNodes, Has.Count.EqualTo(1));
        }

        [Test]
        public void Should_NextNode_NUll_When_Is_LastNode()
        {
            var context = new Context();
            context.Add("text", "example message");

            var fs = new FileStream("flow.json", FileMode.Open, FileAccess.Read);
            string strFlow;
            using (StreamReader reader = new StreamReader(fs))
            {
                strFlow = reader.ReadToEnd();
            }
            var flow = JsonSerializer.Deserialize<Flow>(strFlow);
            var node = NodeService.GetNode(flow.Pages.First().Value,"2");

            Assert.That(context.ContainsKey("NextNodes"), Is.False);
            NodeService.SetNext(flow, node, context, "output_1", true);
            Assert.That(context.ContainsKey("NextNodes"), Is.False);
        }
    }
}