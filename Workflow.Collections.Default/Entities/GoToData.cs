namespace Workflow.NodeSteps.Entities
{
    /// <summary>
    /// The GoToNode data model
    /// </summary>
    public class GoToData
    {
        /// <summary>
        /// The page name
        /// </summary>
        public string PageName { get; set; } = string.Empty;
        /// <summary>
        /// The node Id to be called
        /// </summary>
        public string NodeId { get; set; } = string.Empty;
    }
}
