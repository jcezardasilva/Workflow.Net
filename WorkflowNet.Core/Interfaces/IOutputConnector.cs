using System.Collections.Generic;
using WorkflowNet.Core.Interfaces.Actions;

namespace WorkflowNet.Core.Interfaces
{
    public interface IOutputConnector
    {
        IActionConnector GetOutputConnector();
        void SetOutputConnector(IActionConnector ActionConnector);
        void RemoveOutputConnector();
    }
}
