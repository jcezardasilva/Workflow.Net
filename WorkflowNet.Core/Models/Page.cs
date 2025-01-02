using System.Collections.Generic;
using System.Linq;
using WorkflowNet.Core.Extensions.Lists;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Page : IPage
    {
        private string _id;
        private string _name;
        private string _description;
        private List<IVariable> _variables;
        private List<IAction> _actions;
        private string _startAction;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public IEnumerable<IAction> Actions { get => _actions; set => _actions = value.ToList(); }
        public IEnumerable<IVariable> Variables { get => _variables; set => _variables = value.ToList(); }
        public IAction StartAction { get => _actions.Find(x=> x.Name == _startAction); set => _startAction = value.Name; }

        public void AddAction(IAction action)
        {
            _actions.Add(action);
        }

        public void AddActions(IEnumerable<IAction> actions)
        {
            _actions.AddRange(actions);
        }

        public IAction GetAction(string name)
        {
            return _actions.Find(x => x.Name == name);
        }

        public IEnumerable<IAction> GetActions()
        {
            return _actions;
        }

        public void AddVariable(IVariable variable)
        {
            _variables.Add(variable);
        }

        public void AddVariables(IEnumerable<IVariable> variables)
        {
            _variables.AddRange(variables);
        }

        public IVariable GetVariable(string name)
        {
            return _variables.Find(x=> x.Name == name);
        }

        public IEnumerable<IVariable> GetVariables()
        {
            return _variables;
        }

        public void RemoveAction(IAction action)
        {
            _actions.Remove(action);
        }

        public void RemoveVariable(IVariable variable)
        {
            _variables.Remove(variable);
        }

        public void SetAction(IAction action)
        {
            _actions.Set(x => x.Name == action.Name, action);
        }

        public void SetVariable(IVariable variable)
        {
            _variables.Set(x => x.Name == variable.Name, variable);
        }
    }
}
