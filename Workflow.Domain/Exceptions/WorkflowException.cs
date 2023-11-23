using System;

namespace Workflow.Domain.Exceptions
{
    /// <summary>
    /// A generic workflow exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WorkflowException<T> : Exception
    {
        /// <summary>
        /// Initializes the exception without any parameters
        /// </summary>
        public WorkflowException() { }
        /// <summary>
        /// Initializes the exception with a message
        /// </summary>
        /// <param name="message"></param>
        public WorkflowException(string message) : base(message) { }
        /// <summary>
        /// Initializes the exception with a message and a inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WorkflowException(string message, Exception innerException) : base(message, innerException) { }
    }
}
