using System;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto
{
    public class TicketDto
    {
        public TicketDto(){}
        public TicketDto(Ticket ticket)
        {
            Id = ticket.Id;
            Name = ticket.Name;
            Description = ticket.Description;
            Priority = ticket.Priority;
            Status = ticket.Status;
            Assignee = new UserDto(ticket.Assignee);
            DueDate = ticket.DueDate;
            EstimatedTime = ticket.EstimatedTime;
            ProjectUri = ticket.Project.Uri;
        }
        public Guid? Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public UserDto Assignee { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today;
        public TimeSpan EstimatedTime { get; set; }
        public String ProjectUri { get; set; }
    }
}