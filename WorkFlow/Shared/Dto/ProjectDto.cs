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
            Company = new CompanyDto(project.Company);
        }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Status Status { get; set; } = Status.ToDo;
        public DateTime DueDate { get; set; }
        public String Uri { get; set; }
        public CompanyDto Company { get; set; }
        // public virtual ICollection<Ticket> Tickets { get; set; }
        // public virtual ICollection<User> Users { get; set; }
    }
}