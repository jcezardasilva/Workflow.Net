using System.Collections.Generic;

namespace WorkflowNet.Core.Models
{
    public class ActionConnector
    {
        public string Id { get; set; }
        public IEnumerable<ActionConnection> Connections { get; set; }
    }
}
