using System.Collections.Generic;
using System.Linq;
using WorkflowNet.Core.Extensions.List;
using WorkflowNet.Core.Interfaces;
using WorkflowNet.Core.Interfaces.Actions;

namespace WorkflowNet.Core.Models
{
    public class Action : IAction
    {
        private string _id;
        private string _name;
        private string _description;
        private List<IVariable> _variables;
        private List<IActionConnector> _inputs;
        private List<IActionConnector> _outputs;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public IEnumerable<IVariable> Variables { get => _variables; set => _variables = value.ToList(); }
        public IEnumerable<IActionConnector> Inputs { get => _inputs; set => _inputs = value.ToList(); }
        public IEnumerable<IActionConnector> Outputs { get => _outputs; set => _outputs = value.ToList(); }

        public void AddVariables(IEnumerable<IVariable> variables)
        {
            _variables.AddRange(variables);
        }

        public void AddVariable(IVariable variable)
        {
            _variables.Add(variable);
        }
        public IEnumerable<IVariable> GetVariables()
        {
            return _variables;
        }

        public IVariable GetVariable(string name)
        {
            return _variables.Find(x => x.Name == name);
        }
        public void RemoveVariable(IVariable variable)
        {
            _variables.Remove(variable);
        }
        public void SetVariable(IVariable variable)
        {
            _variables.Set(x => x.Name == variable.Name, variable);
        }
    }
}
