using WorkflowNet.Core.Models;

namespace WorkflowNet.Core.Interfaces
{
    public interface IOutputConnector
    {
        ActionConnector GetOutputConnector();
        void SetOutputConnector(ActionConnector ActionConnector);
        void RemoveOutputConnector();
    }
}
