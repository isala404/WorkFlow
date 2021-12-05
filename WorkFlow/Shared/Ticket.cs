using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Shared
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Priority Priority { get; set; }
        public String Assignee { get; set; }
        public DateTime DueDate { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public Project Project { get; set; }
    }

    public enum Status
    {
        ToDo,
        InProgress,
        Completed
    }
    
    public enum Priority
    {
        LOW,
        MEDIUM,
        HIGH,
    }
}