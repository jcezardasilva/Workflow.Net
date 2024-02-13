using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// A DrawFlow page data model
    /// </summary>
    public class FlowPage
    {
        /// <summary>
        /// The DrawFlow page nodes
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, Node> Data { get; set; } = new Dictionary<string, Node>();
    }
}
