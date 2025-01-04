using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowNet.Core.Interfaces
{
    public interface ISession
    {
        Guid SessionId { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        void StartSession();
        void EndSession();
    }
}
