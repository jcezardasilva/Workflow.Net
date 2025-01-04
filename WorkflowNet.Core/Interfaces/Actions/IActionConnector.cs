using System.Collections.Generic;

namespace WorkflowNet.Core.Interfaces.Actions
{
    public interface IActionConnector
    {
        IEnumerable<IActionConnection> Connections { get; set; }
    }
}
