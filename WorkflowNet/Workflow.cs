using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkflowNet.Core.Interfaces;
using WorkflowNet.Core.Interfaces.Actions;
using WorkflowNet.Core.Models;

namespace WorkflowNet
{
    public class Workflow : IWorkflow
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IWorkflow> _logger;
        private readonly WorkflowSettings _workflowSettings;
        public Workflow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<ILogger<IWorkflow>>();
            _workflowSettings = new WorkflowSettings();
        }
        public Workflow(IServiceProvider serviceProvider, WorkflowSettings workflowSettings)
        {
            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetRequiredService<ILogger<IWorkflow>>();
            _workflowSettings = workflowSettings;
        }
        public IActionHandler GetActionHandler(IAction action)
        {
            var actionHandler = _serviceProvider.GetServices<IActionHandler>().FirstOrDefault(x => x.Name == action.Name);
            if (actionHandler == default)
            {
                throw new Exception($"IActionHandler for {action.Name} not found.");
            }
            return actionHandler;
        }
        public IPage GetStartPage(Context context)
        {
            return context.GetCurrentPage();
        }
        /// <summary>
        /// Get action from output connector. if is not set get the current action first output connection.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IAction GetNextAction(IContext context)
        {
            var connector = context.GetOutputConnector();

            if (connector == default)
                connector = context.GetCurrentAction().Outputs.FirstOrDefault();

            if (connector == default)
                return default;

            foreach (var page in context.GetPackage().GetPages())
            {
                var match = page.GetActions().FirstOrDefault(x => connector.Connections.Any(c => c.ActionId == x.Id));
                if (match != default)
                {
                    context.SetCurrentPage(page);
                    context.RemoveOutputConnector();
                    return match;
                }
            }

            return default;
        }

        public async Task<IContext> ProcessAsync(IContext context)
        {
            IAction action = context.GetCurrentAction();
            context.StartSession();
            while(action != default)
            {
                context = await ProcessNextAsync(context,action);
                action = GetNextAction(context);
            }
            context.EndSession();
            return context;
        }

        public async Task<IContext> ProcessNextAsync(IContext context, IAction action)
        {
            var actionHandler = GetActionHandler(action);
            context.ApplyValues(action.GetVariables());
            context.SetCurrentAction(action);
            if(_workflowSettings.ActiveActionLogs)
                _logger.LogInformation("PreProcess package:{package}|page:{page}|action:{action}|session:{session}",context.GetPackage().Name,context.GetCurrentPage().Name, action.Name, context.SessionId);
            context = await actionHandler.ProcessAsync(context);
            if (_workflowSettings.ActiveActionLogs)
                _logger.LogInformation("PosProcess package:{package}|page:{page}|action:{action}|session:{session}", context.GetPackage().Name, context.GetCurrentPage().Name, action.Name, context.SessionId);
            return context;
        }
    }
}
