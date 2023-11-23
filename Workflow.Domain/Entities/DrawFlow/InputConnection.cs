namespace Workflow.Domain.Entities.DrawFlow
{
    /// <summary>
    /// An node input connnection
    /// </summary>
    public class InputConnection
    {
        /// <summary>
        /// The predecessor node id
        /// </summary>
        public string Node { get; set; } = string.Empty;
        /// <summary>
        /// The predecessor node output name
        /// </summary>
        public string Input { get; set; } = string.Empty;
    }
}
