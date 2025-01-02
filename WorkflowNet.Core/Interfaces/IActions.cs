using System.Collections.Generic;

namespace WorkflowNet.Core.Interfaces
{
    public interface IActions
    {
        IEnumerable<IAction> GetActions();
        IAction GetAction(string name);
        void AddActions(IEnumerable<IAction> actions);
        void AddAction(IAction action);
        void SetAction(IAction action);
        void RemoveAction(IAction action);
    }
}
