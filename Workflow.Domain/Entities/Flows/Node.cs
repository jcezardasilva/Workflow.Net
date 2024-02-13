using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.Flows
{
    /// <summary>
    /// A DrawFlow Node
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The node id
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// The node name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The node data model
        /// </summary>
        [JsonPropertyName("data")]
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// The node class
        /// </summary>
        [JsonPropertyName("class")]
        public string Class { get; set; } = string.Empty;
        /// <summary>
        /// The node HTML
        /// </summary>
        [JsonPropertyName("html")]
        public string Html { get; set; } = string.Empty;
        /// <summary>
        /// The node type
        /// </summary>
        [JsonPropertyName("typenode")]
        public string Typenode { get; set; } = string.Empty;
        /// <summary>
        /// The node inputs
        /// </summary>
        [JsonPropertyName("inputs")]
        public Dictionary<string, NodeInput> Inputs { get; set; } = new Dictionary<string, NodeInput>();
        /// <summary>
        /// The node outputs
        /// </summary>
        [JsonPropertyName("outputs")]
        public Dictionary<string, NodeOutput> Outputs { get; set; } = new Dictionary<string, NodeOutput>();
        /// <summary>
        /// The node horizontal position
        /// </summary>
        [JsonPropertyName("pos_x")]
        public double PosX { get; set; }
        /// <summary>
        /// The node vertical position
        /// </summary>
        [JsonPropertyName("pos_y")]
        public double PosY { get; set; }
    }
}
