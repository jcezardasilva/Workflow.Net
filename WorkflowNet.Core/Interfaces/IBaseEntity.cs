using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowNet.Core.Interfaces
{
    public interface IBaseEntity
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
