using System;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Variable : IVariable
    {
        private string _id;
        private string _name;
        private string _description;
        private object _value;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public Type Type { get; }

        public T Get<T>()
        {
            return (T)_value;
        }

        public void Set<T>(T value)
        {
            _value = value;
        }
    }
}
