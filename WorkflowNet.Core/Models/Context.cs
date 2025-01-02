using System.Collections.Generic;
using System.Text.RegularExpressions;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Context : Dictionary<string, object>, IContext
    {
        private const string CURRENT_PACKAGE = nameof(CURRENT_PACKAGE);
        private const string CURRENT_PAGE = nameof(CURRENT_PAGE);
        private const string CURRENT_ACTION = nameof(CURRENT_ACTION);
        private const string OUTPUT_CONNECTOR = nameof(OUTPUT_CONNECTOR);

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

        public IAction GetCurrentAction()
        {
            return (IAction)this[CURRENT_ACTION];
        }

        public IPage GetCurrentPage()
        {
            return (IPage)this[CURRENT_PAGE];
        }

        public IPackage GetPackage()
        {
            return (IPackage)this[CURRENT_PACKAGE];
        }

        public void SetCurrentAction(IAction action)
        {
            this[CURRENT_ACTION] = action;
        }

        public void SetCurrentPage(IPage page)
        {
            this[CURRENT_PAGE] = page;
        }

        public void SetPackage(IPackage package)
        {
            this[CURRENT_PACKAGE] = package;
        }
    }
}
