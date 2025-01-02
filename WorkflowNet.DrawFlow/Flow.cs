using System.Collections.Generic;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.DrawFlow
{
    public class Flow : IPackage, IBaseEntity
    {
        private Dictionary<string,IPage> _pages = new Dictionary<string,IPage>();
        private Dictionary<string,IVariable> _variables = new Dictionary<string,IVariable>();
        private string _id;
        private string _name;
        private string _description;
        private string _mainPage;
        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public Dictionary<string, IPage> Pages { get => _pages; set => _pages = value; }
        public Dictionary<string, IVariable> Variables { get => _variables; set => _variables = value; }
        public IPage MainPage { get => _pages[_mainPage]; set => _mainPage = value.Name; }

        public void AddPage(IPage page)
        {
            _pages.Add(page.Name, page);
        }

        public void AddPages(IEnumerable<IPage> pages)
        {
            foreach(var page in pages)
            {
                _pages.Add(page.Name,page);
            }
        }

        public void AddVariables(IEnumerable<IVariable> variables)
        {
            foreach(var variable in variables)
            {
                _variables.Add(variable.Name,variable);
            }
        }

        public void AddVariable(IVariable variable)
        {
            _variables.Add(variable.Name,variable);
        }

        public IEnumerable<IPage> GetPages()
        {
            return _pages.Values;
        }

        public IPage GetPage(string name)
        {
            return _pages[name];
        }

        public IVariable GetVariable(string name)
        {
            return _variables[name];
        }

        public void RemovePage(IPage page)
        {
            _pages.Remove(page.Name);
        }

        public void RemoveVariable(IVariable variable)
        {
            _variables.Remove(variable.Name);
        }

        public void SetPage(IPage page)
        {
            _pages[page.Name] = page;
        }

        public void SetVariable(IVariable variable)
        {
            _variables[variable.Name] = variable;
        }

        IEnumerable<IVariable> IVariables.GetVariables()
        {
            return _variables.Values;
        }
    }
}
