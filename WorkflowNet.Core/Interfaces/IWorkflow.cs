using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowNet.Core.Interfaces.Actions;

namespace WorkflowNet.Core.Interfaces
{
    public interface IWorkflow
    {
        Task<IContext> ProcessAsync(IContext context);
        Task<IContext> ProcessNextAsync(IContext context, IAction action);
        IAction GetNextAction(IContext context);
        IActionHandler GetActionHandler(IAction action);
    }
}
