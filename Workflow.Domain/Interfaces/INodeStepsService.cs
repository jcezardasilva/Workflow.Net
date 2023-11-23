namespace Workflow.Domain.Interfaces
{
    /// <summary>
    /// The NodeStepsService interface
    /// </summary>
    public interface INodeStepsService
    {
        /// <summary>
        /// Retrieves a node step
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        INodeStepAsync GetNodeStep(string nodeName);
    }
}