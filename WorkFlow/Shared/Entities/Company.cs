using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow.Shared.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Uri { get; set; }

        public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<UserCompany>? Users { get; set; }
    }
}