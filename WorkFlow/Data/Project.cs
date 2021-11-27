using System;

namespace WorkFlow.Data
{
    public class Project
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today;
        public string Uri { get; set; }
    }
}