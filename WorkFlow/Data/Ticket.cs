using System;

namespace WorkFlow.Data
{
    public class Ticket
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public string Assignee { get; set; }
        public DateTime DueDate { get; set; }
        public TimeSpan EstimatedTime { get; set; }
    }

    public enum Priority
    {
        LOW,
        MEDIUM,
        HIGH,
    }
}