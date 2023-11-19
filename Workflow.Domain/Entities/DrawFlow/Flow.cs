using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    public class Flow
    {
        public Dictionary<string, FlowPage> DrawFlow { get; set; } = new Dictionary<string, FlowPage>();
    }
}
