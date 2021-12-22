﻿using System;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(Company company)
        {
            Id = company.Id;
            Name = company.Name;
            Uri = company.Uri;
        }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Uri { get; set; }
        // public virtual ICollection<Project> Projects { get; set; }
        // public virtual ICollection<UserCompany> Users { get; set; }
    }

    public class UserCompanyDto
    {
        public UserCompanyDto()
        {
        }

        public UserCompanyDto(UserCompany userCompany)
        {
            UserId = userCompany.UserId;
            CompanyId = userCompany.CompanyId;
            Role = userCompany.Role;
        }
        
        public String UserId { get; set; }
        public Guid CompanyId { get; set; }
        public UserRole Role { get; set; }
    }
}