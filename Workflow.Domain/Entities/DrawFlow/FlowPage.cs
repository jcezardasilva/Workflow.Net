using System.Collections.Generic;

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
        public Dictionary<string, Node> Data { get; set; } = new Dictionary<string, Node>();
    }
}
