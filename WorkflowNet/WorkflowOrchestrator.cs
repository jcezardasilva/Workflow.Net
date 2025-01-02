using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet
{
    public class WorkflowOrchestrator
    {
        private readonly IServiceProvider _serviceProvider;
        public WorkflowOrchestrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IActionHandler GetActionHandler(IAction action)
        {
            var actionHandler = _serviceProvider.GetServices<IActionHandler>().FirstOrDefault(x=> x.Name == action.Name);
            if(actionHandler == default)
            {
                throw new Exception($"IActionHandler for {action.Name} not found.");
            }
            return actionHandler;
        }
    }
}
