using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.DrawFlow;
using Workflow.Domain.Entities;

namespace Workflow.Domain.Interfaces
{
    public interface INodeStepAsync
    {
        Task<Context> ProcessAsync(Flow flow, Node node, Context context);
    }
}
