using System.Text.Json.Serialization;

namespace Workflow.NodeSteps.Entities
{
    public class HttpRequestData
    {
        public string Url { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public string OutputStatus {  get; set; } = string.Empty;
        public string OutputContentType { get; set; } = string.Empty;
        public string OutputContent { get; set; } = string.Empty;
    }
}
