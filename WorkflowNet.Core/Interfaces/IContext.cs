using System.Collections.Generic;
using WorkflowNet.Core.Models;

namespace WorkflowNet.Core.Interfaces
{
    public interface IContext : IDictionary<string, object>,IOutputConnector, ISession
    {
        void SetPackage(Package package);
        Package GetPackage();
        void SetCurrentPage(Page page);
        Page GetCurrentPage();
        void SetCurrentAction(Action action);
        Action GetCurrentAction();
        Dictionary<string, object> ApplyValues(Dictionary<string,object> variables);
        string ApplyValues(string variable);
    }
}
