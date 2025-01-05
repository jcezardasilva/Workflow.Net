using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowNet.Core.Interfaces;
using WorkflowNet.Core.Models;
using Action = WorkflowNet.Core.Models.Action;

namespace WorkflowNet
{
    public class Workflow
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
            _workflowSettings = workflowSettings;
            _logger = workflowSettings.ActiveActionLogs ? serviceProvider.GetRequiredService<ILogger<IWorkflow>>() : default;
        }
        public IActionHandler GetActionHandler(Action action)
        {
            var actionHandler = _serviceProvider.GetServices<IActionHandler>().FirstOrDefault(x => x.GetType().Name == action.ActionHandler.Name);
            if (actionHandler == default)
            {
                throw new Exception($"IActionHandler for {action.Name} not found.");
            }
            return actionHandler;
        }
        public Page GetStartPage(Context context)
        {
            return context.GetCurrentPage();
        }
        /// <summary>
        /// Get action from output connector. if is not set get the current action first output connection.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Action GetNextAction(IContext context)
        {
            var connector = context.GetOutputConnector();

            if (connector == default)
                connector = context.GetCurrentAction().Outputs.FirstOrDefault();

            if (connector == default)
                return default;

            foreach (var page in context.GetPackage().Pages)
            {
                var match = page.Actions.FirstOrDefault(x => connector.Connections.Any(c => c.ActionId == x.Id));
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
            Action action = context.GetCurrentAction();
            context.StartSession();
            while(action != default)
            {
                context = await ProcessNextAsync(context,action);
                action = GetNextAction(context);
            }
            context.EndSession();
            return context;
        }

        public async Task<IContext> ProcessNextAsync(IContext context, Action action)
        {
            var actionHandler = GetActionHandler(action);
            context.ApplyValues(action.Variables);
            context.SetCurrentAction(action);
            if(_workflowSettings.ActiveActionLogs)
                _logger.LogInformation("PreProcess package:{package}|page:{page}|action:{action}|session:{session}",context.GetPackage().Name,context.GetCurrentPage().Name, action.Name, context.SessionId);
            context = await actionHandler.ProcessAsync(context);
            if (_workflowSettings.ActiveActionLogs)
                _logger.LogInformation("PosProcess package:{package}|page:{page}|action:{action}|session:{session}", context.GetPackage().Name, context.GetCurrentPage().Name, action.Name, context.SessionId);
            return context;
        }

        public async Task<IContext> ProcessAsync(Package package)
        {
            var context = new Context();
            context.SetPackage(package);
            return await ProcessAsync(context);
        }

        public async Task<IContext> ProcessAsync(Package package, IDictionary<string, object> inputData)
        {
            var context = new Context();
            context.SetPackage(package);
            foreach(var item in inputData)
            {
                context.Add(item.Key, item.Value);
            }
            return await ProcessAsync(context);
        }
    }
}
