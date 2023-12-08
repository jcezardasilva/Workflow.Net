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
        /// Environment variables
        /// </summary>
        public Dictionary<string, string> Environment { get; set; } = new Dictionary<string, string>();
    }
}
