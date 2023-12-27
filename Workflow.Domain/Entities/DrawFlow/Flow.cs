using System.Collections.Generic;

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
        public Dictionary<string, FlowPage> DrawFlow { get; set; } = new Dictionary<string, FlowPage>();
        /// <summary>
        /// The flow ID
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The flow name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The flow description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Environment variables
        /// </summary>
        public Dictionary<string, string> Environment { get; set; } = new Dictionary<string, string>();
    }
}
