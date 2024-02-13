using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("connections")]
        public List<InputConnection> Connections { get; set; } = new List<InputConnection>();
    }
}
