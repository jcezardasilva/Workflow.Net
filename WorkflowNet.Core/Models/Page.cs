using System.Collections.Generic;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Page: IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public IEnumerable<Action> Actions { get; set; }
        public string StartAction { get; set; }
    }
}
