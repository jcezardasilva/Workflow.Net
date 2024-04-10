using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.Flows
{
    /// <summary>
    /// The Flow package
    /// </summary>
    public class Flow
    {
        /// <summary>
        /// The Workflow pages
        /// </summary>
        [JsonPropertyName("pages")]
        public Dictionary<string, FlowPage> Pages { get; set; } = new Dictionary<string, FlowPage>();
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
        /// The default flow page to be called at the workflow startup.
        /// </summary>
        [JsonPropertyName("mainPage")]
        public string MainPage { get; set; } = "Home";
        /// <summary>
        /// Package variables
        /// </summary>
        [JsonPropertyName("variables")]
        public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
    }
}
