using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Shared
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Uri { get; set; }
        public User[] Users { get; set; }
        public Project[] Projects { get; set; }
    }
}