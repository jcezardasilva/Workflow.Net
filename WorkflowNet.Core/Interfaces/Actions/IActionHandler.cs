using System.Threading.Tasks;

namespace WorkflowNet.Core.Interfaces.Actions
{
    public interface IActionHandler : IAction
    {
        Task<IContext> ProcessAsync(IContext context);
    }
}
