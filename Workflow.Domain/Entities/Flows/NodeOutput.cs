using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.Flows
{
    /// <summary>
    /// A node output
    /// </summary>
    public class NodeOutput
    {
        /// <summary>
        /// The node output connections
        /// </summary>
        [JsonPropertyName("connections")]
        public List<OutputConnection> Connections { get; set; } = new List<OutputConnection>();
    }
}
