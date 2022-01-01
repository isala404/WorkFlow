using System;
using System.Collections.Generic;
using System.Linq;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(Company company, bool expand = true)
        {
            Id = company.Id;
            Name = company.Name;
            Uri = company.Uri;
            
            if (!expand) return;
            
            if (company.Users != null)
                Users = company.Users.Select(c => new UserCompanyDto(c)).ToList();
            
        }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Uri { get; set; }
        // public virtual ICollection<Project> Projects { get; set; }
        public List<UserCompanyDto>? Users { get; set; }
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

            if (userCompany.User != null) User = new UserDto(userCompany.User, false);
            if (userCompany.Company != null) Company = new CompanyDto(userCompany.Company, false);
        }
        
        public String UserId { get; set; }
        public Guid CompanyId { get; set; }
        public UserRole Role { get; set; }
        public UserDto User { get; set; }
        public CompanyDto Company { get; set; }
    }
}