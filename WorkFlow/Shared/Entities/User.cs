using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WorkFlow.Shared.Entities
{
    public class User: IdentityUser
{
        public String? Name { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<UserCompany> Companies { get; set; }
}
    
}