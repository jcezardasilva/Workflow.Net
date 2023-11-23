using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// A node output
    /// </summary>
    public class NodeOutput
    {
        /// <summary>
        /// The node output connections
        /// </summary>
        public List<OutputConnection> Connections { get; set; } = new List<OutputConnection>();
    }
}
