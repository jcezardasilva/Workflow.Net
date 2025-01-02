using System.Collections.Generic;

namespace WorkflowNet.Core.Interfaces
{
    public interface IContext : IDictionary<string, object>
    {
        void SetPackage(IPackage package);
        IPackage GetPackage();
        void SetCurrentPage(IPage page);
        IPage GetCurrentPage();
        void SetCurrentAction(IAction action);
        IAction GetCurrentAction();
        IEnumerable<IVariable> ApplyValues(IEnumerable<IVariable> variables);
        IVariable ApplyValues(IVariable variable);
    }
}
