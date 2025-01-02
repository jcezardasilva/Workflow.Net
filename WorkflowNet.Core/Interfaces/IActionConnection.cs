using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowNet.Core.Interfaces
{
    public interface IActionConnection
    {
        string ActionId { get; set; }
        string ConnectorId { get; set; }
    }
}
