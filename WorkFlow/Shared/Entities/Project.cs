using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Shared.Entities
{
    public class Project
    {
        [Key] public Guid Id { get; set; }
        [Required] public String Name { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public DateTime DueDate { get; set; }
        [Required] public String Uri { get; set; }
        [Required] public Company? Company { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<User>? Users { get; set; } = new List<User>();
    }
}