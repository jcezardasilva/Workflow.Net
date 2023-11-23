using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// A DrawFlow Node
    /// </summary>
    public class Node
    {
        /// <summary>
        /// The node id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The node name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The node data model
        /// </summary>
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The node class
        /// </summary>
        public string Class { get; set; } = string.Empty;
        /// <summary>
        /// The node HTML
        /// </summary>
        public string Html { get; set; } = string.Empty;
        /// <summary>
        /// The node type
        /// </summary>
        public string Typenode { get; set; } = string.Empty;
        /// <summary>
        /// The node inputs
        /// </summary>
        public Dictionary<string, NodeInput> Inputs { get; set; } = new Dictionary<string, NodeInput>();
        /// <summary>
        /// The node outputs
        /// </summary>
        public Dictionary<string, NodeOutput> Outputs { get; set; } = new Dictionary<string, NodeOutput>();
        /// <summary>
        /// The node horizontal position
        /// </summary>
        public int PosX { get; set; }
        /// <summary>
        /// The node vertical position
        /// </summary>
        public int PosY { get; set; }
    }
}
