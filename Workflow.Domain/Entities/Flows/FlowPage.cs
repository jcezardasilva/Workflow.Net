using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.Flows
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
        /// <summary>
        /// The page ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The page name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The page description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// The start node ID
        /// </summary>
        [JsonPropertyName("startNodeId")]
        public string StartNodeId { get; set; } = string.Empty;
        /// <summary>
        /// Environment variables
        /// </summary>
        [JsonPropertyName("environment")]
        public Dictionary<string, object> Environment { get; set; } = new Dictionary<string, object>();
    }
}
