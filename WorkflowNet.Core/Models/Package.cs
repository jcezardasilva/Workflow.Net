using System.Collections.Generic;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Package : IBaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Page> Pages { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public string MainPage { get; set; }
    }
}
