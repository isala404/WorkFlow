using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Shared.Entities
{
    public class Ticket
    {
        [Key] public Guid Id { get; set; }
        [Required] public String Name { get; set; }
        [Required] public String Description { get; set; }
        [Required] public Priority Priority { get; set; }
        [Required] public Status Status { get; set; } = Status.ToDo;
        [Required] public User? Assignee { get; set; }
        [Required] public DateTime DueDate { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        [Required] public Project? Project { get; set; }
    }
}