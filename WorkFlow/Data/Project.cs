using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Data
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }
        public DateTime DueDate { get; set; }
        public String Uri { get; set; }
        public Company Company { get; set; }
        public Ticket[] Tickets { get; set; }
        public User[] Users { get; set; }
    }
}