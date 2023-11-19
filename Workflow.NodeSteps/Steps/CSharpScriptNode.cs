﻿using Microsoft.CodeAnalysis.CSharp.Scripting;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Interfaces;
using Workflow.Nodes;
using Workflow.NodeSteps.Entities;

namespace Workflow.NodeSteps.Steps
{
    public class CSharpScriptNode : INodeStepAsync
    {
        public async Task<Context> ProcessAsync(Flow flow, Node node,Context context)
        {
            var data = NodeService.GetData<ScriptData>(node);

            var code = NodeService.SetValues(data.Code, context);
            var scriptResult = await CSharpScript.EvaluateAsync<string>(code);

            context.Add(data.Output, scriptResult);

            NodeService.SetNext(flow, node, context, "output_1");

            return context;
        }
    }
}
