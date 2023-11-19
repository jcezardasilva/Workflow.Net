using System;
using System.Collections.Generic;
using System.Text;

namespace Workflow.Domain.Exceptions
{
    public class WorkflowException<T> : Exception
    {
        public WorkflowException() { }
        public WorkflowException(string message) : base(message) { }
        public WorkflowException(string message, Exception innerException) : base(message, innerException) { }
    }
}
