using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Shared.Entities
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public User Assignee { get; set; }
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