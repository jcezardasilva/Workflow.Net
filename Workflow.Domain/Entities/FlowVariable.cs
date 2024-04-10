using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Enums;

namespace Workflow.Domain.Entities
{
    /// <summary>
    /// A pattern to add variables to the flow
    /// </summary>
    public class FlowVariable
    {
        /// <summary>
        /// The variable name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The variable value
        /// </summary>
        public string Value {  get; set; } = string.Empty;
        /// <summary>
        /// The variable data type
        /// </summary>
        public VariableDataTypes Type { get; set; } = VariableDataTypes.String;
        /// <summary>
        /// The variable visibility
        /// </summary>
        public bool Visible { get; set; } = true;
    }
}
