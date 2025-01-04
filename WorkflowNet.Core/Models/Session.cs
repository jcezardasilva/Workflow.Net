using System;
using System.Collections.Generic;
using System.Text;
using WorkflowNet.Core.Interfaces;

namespace WorkflowNet.Core.Models
{
    public class Session : ISession
    {
        private Guid _sessionId;
        private DateTime _startTime;
        private DateTime _endTime;

        public Guid SessionId { get => _sessionId; set => _sessionId = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public DateTime EndTime { get => _endTime; set => _endTime = value; }

        public void StartSession()
        {
            _sessionId = Guid.NewGuid();
            _startTime = DateTime.Now;
        }
        public void EndSession()
        {
            _endTime = DateTime.Now;
        }
    }
}
