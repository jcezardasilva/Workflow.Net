namespace Workflow.NodeSteps.Entities
{
    /// <summary>
    /// The HttpRequestNode data model
    /// </summary>
    public class HttpRequestData
    {
        /// <summary>
        /// The URL to be called
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// The HTTP method
        /// </summary>
        public string Method { get; set; } = string.Empty;
        /// <summary>
        /// The node output status variable name
        /// </summary>
        public string OutputStatus {  get; set; } = string.Empty;
        /// <summary>
        /// The node output contenttype variable name
        /// </summary>
        public string OutputContentType { get; set; } = string.Empty;
        /// <summary>
        /// The node output content variable name
        /// </summary>
        public string OutputContent { get; set; } = string.Empty;
    }
}
