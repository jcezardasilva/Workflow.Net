using System.Text.Json.Serialization;

namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// An node input connnection
    /// </summary>
    public class InputConnection
    {
        /// <summary>
        /// The predecessor node id
        /// </summary>
        [JsonPropertyName("node")]
        public string Node { get; set; } = string.Empty;
        /// <summary>
        /// The predecessor node output name
        /// </summary>
        [JsonPropertyName("input")]
        public string Input { get; set; } = string.Empty;
    }
}
