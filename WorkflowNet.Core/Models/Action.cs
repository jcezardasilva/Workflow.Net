using System;
using System.Collections.Generic;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Action : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type ActionHandler { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public IEnumerable<ActionConnector> Inputs { get; set; }
        public IEnumerable<ActionConnector> Outputs { get; set; }
    }
}
