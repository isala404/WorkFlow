using System;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto
{
    public class ProjectDto
    {
        public ProjectDto(){}
        public ProjectDto(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Status = project.Status;
            DueDate = project.DueDate;
            Uri = project.Uri;
            try
            {
                Company = new CompanyDto(project.Company);
                NumOfUsers = project.Users.Count;

                foreach (var ticket in project.Tickets)
                {
                    switch (ticket.Status)
                    {
                        case Status.ToDo:
                            ToDoTickets += 1;
                            break;
                        case Status.InProgress:
                            InProgressTickets += 1;
                            break;
                        case Status.Completed:
                            CompletedTickets += 1;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                // Ignore
            }
        }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public DateTime DueDate { get; set; } = DateTime.Today;
        public String Uri { get; set; }
        public CompanyDto Company { get; set; }

        public int ToDoTickets { get; set; } = 0;
        public int InProgressTickets { get; set; } = 0;
        public int CompletedTickets { get; set; } = 0;
        public int NumOfUsers { get; set; } = 0;
        // public virtual ICollection<Ticket> Tickets { get; set; }
        // public virtual ICollection<User> Users { get; set; }
    }
}