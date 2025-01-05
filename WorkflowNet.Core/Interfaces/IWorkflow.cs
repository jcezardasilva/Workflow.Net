using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowNet.Core.Models;
using Action = WorkflowNet.Core.Models.Action;

namespace WorkflowNet.Core.Interfaces
{
    public interface IWorkflow
    {
        Task<IContext> ProcessAsync(Package package);
        Task<IContext> ProcessAsync(Package package, IDictionary<string,object> inputData);
        Task<IContext> ProcessAsync(IContext context);
        Task<IContext> ProcessNextAsync(IContext context, Action action);
        Action GetNextAction(IContext context);
        IActionHandler GetActionHandler(Action action);
    }
}
