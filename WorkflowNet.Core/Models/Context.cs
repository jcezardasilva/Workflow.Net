using System;
using System.Collections.Generic;
using System.Linq;
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
        private Guid _id;
        private DateTime _startTime;
        private DateTime _endTime;

        public Guid SessionId { get => _id; set => _id = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }

        public Dictionary<string,object> ApplyValues(Dictionary<string,object> variables)
        {
            var output = new Dictionary<string, object>();
            foreach (var variable in variables)
            {
                if(variable.Value.GetType() == typeof(Dictionary<string,object>))
                {
                    var nestedOutput = ApplyValues((Dictionary<string, object>)variable.Value);
                    foreach(var nestedVariable in nestedOutput)
                    {
                        output.Add(nestedVariable.Key, nestedVariable.Value);
                    }
                    continue;
                }
                if (variable.GetType() == typeof(string))
                {
                    output.Add(variable.Key, ApplyValues((string)variable.Value));
                    continue;
                }
                output.Add(variable.Key,variable.Value);
            }
            return output;
        }
        public string ApplyValues(string variable)
        {
            var regex = new Regex("\\{\\{([^}]+)\\}\\}");
            MatchCollection matches = regex.Matches(variable);
            foreach (Match match in matches)
            {
                if(TryGetValue(match.Groups[1].Value, out object value))
                {
                    variable = variable.Replace(match.Value, (string)value);
                }
            }
            return variable;
        }

        public void EndSession()
        {
            _endTime = DateTime.Now;
        }

        public Action GetCurrentAction()
        {
            if (ContainsKey(CURRENT_ACTION))
                return (Action)this[CURRENT_ACTION];

            var page = GetCurrentPage();
            return page.Actions.First(a=> a.Name == page.StartAction);
        }

        public Page GetCurrentPage()
        {
            if(ContainsKey(CURRENT_PAGE))
                return (Page)this[CURRENT_PAGE];

            var package = GetPackage();
            return package.Pages.First(p=> p.Name == package.MainPage);
        }

        public ActionConnector GetOutputConnector()
        {
            if (ContainsKey(OUTPUT_CONNECTOR))
                return (ActionConnector)this[OUTPUT_CONNECTOR];

            return default;
        }

        public Package GetPackage()
        {
            return (Package)this[CURRENT_PACKAGE];
        }

        public void RemoveOutputConnector()
        {
            Remove(OUTPUT_CONNECTOR);
        }

        public void SetCurrentAction(Action action)
        {
            this[CURRENT_ACTION] = action;
        }

        public void SetCurrentPage(Page page)
        {
            this[CURRENT_PAGE] = page;
        }

        public void SetOutputConnector(ActionConnector ActionConnector)
        {
            this[OUTPUT_CONNECTOR] = ActionConnector;
        }

        public void SetPackage(Package package)
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
