using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WorkflowNet.Core.Enums;

namespace WorkflowNet.Core.Interfaces
{
    public interface IActionConnector
    {
        IEnumerable<IActionConnection> Connections { get; set; }
        ConnectorDirection Direction { get; set; }
    }
}
