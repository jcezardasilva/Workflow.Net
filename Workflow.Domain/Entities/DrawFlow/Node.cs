using System.Collections.Generic;

namespace Workflow.Domain.Entities.DrawFlow
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        public string Class { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
        public string Typenode { get; set; } = string.Empty;
        public Dictionary<string, NodeInput> Inputs { get; set; } = new Dictionary<string, NodeInput>();
        public Dictionary<string, NodeOutput> Outputs { get; set; } = new Dictionary<string, NodeOutput>();
        public int PosX { get; set; }
        public int PosY { get; set; }
    }
}
