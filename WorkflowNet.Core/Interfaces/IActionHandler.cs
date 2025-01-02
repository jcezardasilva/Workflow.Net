using System.Threading.Tasks;

namespace WorkflowNet.Core.Interfaces
{
    public interface IActionHandler: IAction
    {
        Task<IContext> ProcessAsync(IContext context);
    }
}
