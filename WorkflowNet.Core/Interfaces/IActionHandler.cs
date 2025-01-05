using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowNet.Core.Interfaces
{
    public interface IActionHandler
    {
        Task<IContext> ProcessAsync(IContext context);
    }
}
