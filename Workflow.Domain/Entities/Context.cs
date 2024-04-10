using System.Collections.Generic;
using Workflow.Domain.Exceptions;

namespace Workflow.Domain.Entities
{
    /// <summary>
    /// The workflow data context
    /// </summary>
    public class Context
    {
        private readonly Dictionary<string, object> _map;
        /// <summary>
        /// Initializes the data context
        /// </summary>
        public Context()
        {
            _map = new Dictionary<string, object>();
        }
        /// <summary>
        /// Add a value on data context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add<T>(string key, T value) where T : notnull
        {
            _map.Add(key, value);
        }
        /// <summary>
        /// Recover a value from data context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{Context}"></exception>
        public T Get<T>(string key)
        {
            if (!_map.ContainsKey(key))
            {
                throw new WorkflowException<Context>($"Inexistent key: {key}.");
            }
            return (T)_map[key];
        }
        /// <summary>
        /// Removes a value from data context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{Context}"></exception>
        public void Delete<T>(string key)
        {
            if (!_map.ContainsKey(key))
            {
                throw new WorkflowException<Context>($"Inexistent key: {key}.");
            }
            _map.Remove(key);
        }
        /// <summary>
        /// Updates a value from data context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{Context}"></exception>
        public void Update<T>(string key, T newValue) where T : notnull
        {
            if (!_map.ContainsKey(key))
            {
                throw new WorkflowException<Context>($"Inexistent key: {key}.");
            }
            _map[key] = newValue;
        }
        /// <summary>
        /// Updates a value from data context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException{Context}"></exception>
        public void Upsert<T>(string key, T newValue) where T : notnull
        {
            if (_map.ContainsKey(key))
            {
                _map[key] = newValue;
            }
            else
            {
                _map.Add(key, newValue);
            }
        }
        /// <summary>
        /// Check if a key exists on data context
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _map.ContainsKey(key);
        }
        /// <summary>
        /// Tries recover a reference value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet<T>(string key, out T? value)
        {
            if (_map.TryGetValue(key, out var obj) && obj is T t)
            {
                value = t;
                return true;
            }

            value = default;
            return false;
        }
        /// <summary>
        /// Exports the context data as a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,object> ToDictionary()
        {
            return _map;
        }
        /// <summary>
        /// Adds multiple itens to the context
        /// </summary>
        /// <param name="values"></param>
        public void AddRange(Dictionary<string,object> values)
        {
            foreach(var item in values)
            {
                _map.Add(item.Key, item.Value);
            }
        }
    }
}
