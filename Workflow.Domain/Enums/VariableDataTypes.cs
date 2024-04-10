using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Enums
{
    /// <summary>
    /// The supported variable data types
    /// </summary>
    public enum VariableDataTypes
    {
        /// <summary>
        /// true or false
        /// </summary>
        Boolean,
        /// <summary>
        /// A byte chain
        /// </summary>
        Bytes,
        /// <summary>
        /// A single character
        /// </summary>
        Char,
        /// <summary>
        /// A decimal floating-point number
        /// </summary>
        Decimal,
        /// <summary>
        /// A 32-bit signed integer
        /// </summary>
        Integer,
        /// <summary>
        /// A 64-bit signed intege
        /// </summary>
        Long,
        /// <summary>
        /// Any class instance
        /// </summary>
        Object,
        /// <summary>
        /// A sequence of characters
        /// </summary>
        String,
    }
}
