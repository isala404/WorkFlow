using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WorkFlow.Shared.Entities
{
    public class User : IdentityUser
    {
        public String? Name { get; set; }
        public ICollection<Project>? Projects { get; set; } = new List<Project>();
        public virtual ICollection<UserCompany>? Companies { get; set; } = new List<UserCompany>();
    }
}