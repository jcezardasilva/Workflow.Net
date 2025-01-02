using System.Collections.Generic;
using System.Linq;
using WorkflowNet.Core.Extensions.Lists;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Package : IPackage, IBaseEntity
    {
        private List<IPage> _pages;
        private List<IVariable> _variables;

        private string _id;
        private string _name;
        private string _description;
        private string _mainPage;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public IEnumerable<IPage> Pages { get => _pages; set => _pages = value.ToList(); }
        public IEnumerable<IVariable> Variables {  get => _variables; set => _variables = value.ToList(); }
        public IPage MainPage { get => _pages.First(x => x.Name == _mainPage); set => _mainPage = value.Name; }

        public void AddPage(IPage page)
        {
            _pages.Add(page);
        }

        public void AddPages(IEnumerable<IPage> pages)
        {
            _pages.AddRange(pages);
        }

        public void AddVariables(IEnumerable<IVariable> variables)
        {
            _variables.AddRange(variables);
        }

        public void AddVariable(IVariable variable)
        {
            _variables.Add(variable);
        }

        public IPage GetPage(string name)
        {
            return _pages.Find(x => x.Name == name);
        }

        public IEnumerable<IPage> GetPages()
        {
            return _pages;
        }

        public IEnumerable<IVariable> GetVariables()
        {
            return _variables;
        }

        public IVariable GetVariable(string name)
        {
            return _variables.Find(x => x.Name == name);
        }

        public void RemovePage(IPage page)
        {
            _pages.Remove(page);
        }

        public void RemoveVariable(IVariable variable)
        {
            _variables.Remove(variable);
        }

        public void SetPage(IPage page)
        {
            _pages.Set(x => x.Name == page.Name, page);
        }

        public void SetVariable(IVariable variable)
        {
            _variables.Set(x=> x.Name == variable.Name, variable);
        }
    }
}
