using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    public class NodeInput
    {
        public List<InputConnection> Connections { get; set; } = new List<InputConnection>();
    }
}
