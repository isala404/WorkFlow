using System;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto
{
    public class TicketDto
    {
        public TicketDto()
        {
        }

        public TicketDto(Ticket ticket)
        {
            Id = ticket.Id;
            Name = ticket.Name;
            Description = ticket.Description;
            Priority = ticket.Priority;
            Status = ticket.Status;
            DueDate = ticket.DueDate;
            EstimatedTime = ticket.EstimatedTime.Hours;
            TicketUrl = $"{ticket.Project?.Company?.Uri}/project/{ticket.Project?.Uri}/ticket/{ticket.Id}/manage";
            if (ticket.Assignee != null) Assignee = new UserDto(ticket.Assignee);
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public UserDto Assignee { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today;
        public Int32 EstimatedTime { get; set; }
        public String TicketUrl { get; set; }
    }
}