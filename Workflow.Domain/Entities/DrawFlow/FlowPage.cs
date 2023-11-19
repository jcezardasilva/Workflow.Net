using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    public class FlowPage
    {
        public Dictionary<string, Node> Data { get; set; } = new Dictionary<string, Node>();
    }
}
