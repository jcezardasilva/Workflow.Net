namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// A node output connection
    /// </summary>
    public class OutputConnection
    {
        /// <summary>
        /// The next node id
        /// </summary>
        public string Node { get; set; } = string.Empty;
        /// <summary>
        /// The next node input name
        /// </summary>
        public string Output { get; set; } = string.Empty;
    }
}
