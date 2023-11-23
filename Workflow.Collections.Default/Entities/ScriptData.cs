using System.Text.Json.Serialization;

namespace Workflow.NodeSteps.Entities
{
    /// <summary>
    /// The ScriptNode data model
    /// </summary>
    public class ScriptData
    {
        /// <summary>
        /// The script code
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// The output variable name
        /// </summary>
        public string Output { get; set; } = string.Empty;
    }
}
