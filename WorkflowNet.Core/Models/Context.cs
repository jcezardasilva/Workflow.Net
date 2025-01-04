using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WorkflowNet.Core.Interfaces;
using WorkflowNet.Core.Interfaces.Actions;

namespace WorkflowNet.Core.Models
{
    public class Context : Dictionary<string, object>, IContext
    {
        private const string CURRENT_PACKAGE = nameof(CURRENT_PACKAGE);
        private const string CURRENT_PAGE = nameof(CURRENT_PAGE);
        private const string CURRENT_ACTION = nameof(CURRENT_ACTION);
        private const string OUTPUT_CONNECTOR = nameof(OUTPUT_CONNECTOR);
        private Guid _id;
        private DateTime _startTime;
        private DateTime _endTime;

        public Guid SessionId { get => _id; set => _id = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }

        public IEnumerable<IVariable> ApplyValues(IEnumerable<IVariable> variables)
        {
            var output = new List<IVariable>();
            foreach (var variable in variables)
            {
                if(variable.Type == typeof(IEnumerable<IVariable>))
                {
                    var nestedVariables = variable.Get<IEnumerable<IVariable>>();
                    output.AddRange(ApplyValues(nestedVariables));
                    continue;
                }
                if (variable.Type == typeof(string))
                {
                    output.Add(ApplyValues(variable));
                    continue;
                }
                output.Add(variable);
            }
            return output;
        }
        public IVariable ApplyValues(IVariable variable)
        {
            var output = new Variable();
            var regex = new Regex("\\{\\{([^}]+)\\}\\}");
            var variableValue = variable.Get<string>();
            MatchCollection matches = regex.Matches(variableValue);
            foreach (Match match in matches)
            {
                if(TryGetValue(match.Groups[1].Value, out object value))
                {
                    variableValue = variableValue.Replace(match.Value, (string)value);
                }
            }
            output.Set(variableValue);
            return output;
        }

        public void EndSession()
        {
            _endTime = DateTime.Now;
        }

        public IAction GetCurrentAction()
        {
            if (ContainsKey(CURRENT_ACTION))
                return (IAction)this[CURRENT_ACTION];

            return GetCurrentPage().StartAction;
        }

        public IPage GetCurrentPage()
        {
            if(ContainsKey(CURRENT_PAGE))
                return (IPage)this[CURRENT_PAGE];

            return GetPackage().MainPage;
        }

        public IActionConnector GetOutputConnector()
        {
            if (ContainsKey(OUTPUT_CONNECTOR))
                return (IActionConnector)this[OUTPUT_CONNECTOR];

            return default;
        }

        public IPackage GetPackage()
        {
            return (IPackage)this[CURRENT_PACKAGE];
        }

        public void RemoveOutputConnector()
        {
            Remove(OUTPUT_CONNECTOR);
        }

        public void SetCurrentAction(IAction action)
        {
            this[CURRENT_ACTION] = action;
        }

        public void SetCurrentPage(IPage page)
        {
            this[CURRENT_PAGE] = page;
        }

        public void SetOutputConnector(IActionConnector ActionConnector)
        {
            this[OUTPUT_CONNECTOR] = ActionConnector;
        }

        public void SetPackage(IPackage package)
        {
            this[CURRENT_PACKAGE] = package;
        }

        public void StartSession()
        {
            _id = Guid.NewGuid();
            _startTime = DateTime.Now;
        }
    }
}
