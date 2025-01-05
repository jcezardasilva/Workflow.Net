using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowNet.Collections.Example.Models
{
    public class HttpGetActionModel
    {
        /// <summary>
        /// The URL to be called
        /// </summary>
        public string RequestUri { get; set; } = string.Empty;
        /// <summary>
        /// The response status variable name
        /// </summary>
        public string ResponseStatus { get; set; } = string.Empty;
        /// <summary>
        /// The response contenttype variable name
        /// </summary>
        public string ResponseContentType { get; set; } = string.Empty;
        /// <summary>
        /// The response content variable name
        /// </summary>
        public string ResponseContent { get; set; } = string.Empty;
    }
}
