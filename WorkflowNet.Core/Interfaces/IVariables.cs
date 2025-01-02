using System.Collections.Generic;

namespace WorkflowNet.Core.Interfaces
{
    public interface IVariables
    {
        IEnumerable<IVariable> GetVariables();
        IVariable GetVariable(string name);
        void AddVariables(IEnumerable<IVariable> variables);
        void AddVariable(IVariable variable);
        void SetVariable(IVariable variable);
        void RemoveVariable(IVariable variable);
    }
}
