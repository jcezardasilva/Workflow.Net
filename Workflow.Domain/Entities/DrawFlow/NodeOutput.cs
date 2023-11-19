using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    public class NodeOutput
    {
        public List<OutputConnection> Connections { get; set; } = new List<OutputConnection>();
    }
}
