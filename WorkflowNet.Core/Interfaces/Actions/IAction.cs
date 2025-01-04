using System.Collections.Generic;

namespace WorkflowNet.Core.Interfaces.Actions
{
    public interface IAction : IBaseEntity, IVariables
    {
        IEnumerable<IActionConnector> Inputs { get; set; }
        IEnumerable<IActionConnector> Outputs { get; set; }
    }
}
