using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.DrawFlow;

namespace Workflow.Domain.Interfaces
{
    /// <summary>
    /// The workflow node step interface
    /// </summary>
    public interface INodeStepAsync
    {
        /// <summary>
        /// Executes the step
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="node"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<Context> ProcessAsync(Flow flow, Node node, Context context);
    }
}
