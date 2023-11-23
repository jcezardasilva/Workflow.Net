using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// A node input
    /// </summary>
    public class NodeInput
    {
        /// <summary>
        /// The node input connections
        /// </summary>
        public List<InputConnection> Connections { get; set; } = new List<InputConnection>();
    }
}
