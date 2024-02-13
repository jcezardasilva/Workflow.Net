using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// The DrawFlow data model
    /// </summary>
    public class Flow
    {
        /// <summary>
        /// The DrawFlow pages
        /// </summary>
        [JsonPropertyName("drawflow")]
        public Dictionary<string, FlowPage> DrawFlow { get; set; } = new Dictionary<string, FlowPage>();
        /// <summary>
        /// The flow ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The flow name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The flow description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// The start node ID
        /// </summary>
        [JsonPropertyName("startNodeId")]
        public string StartNodeId { get; set; } = string.Empty;
        /// <summary>
        /// The default flow to be called at the workflow startup.
        /// </summary>
        [JsonPropertyName("mainflow")]
        public string MainFlow {  get; set; } = "Home";
        /// <summary>
        /// Environment variables
        /// </summary>
        [JsonPropertyName("environment")]
        public Dictionary<string, string> Environment { get; set; } = new Dictionary<string, string>();
    }
}
