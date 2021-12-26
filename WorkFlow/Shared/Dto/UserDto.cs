using System;
using System.Collections;
using System.Collections.Generic;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto
{
    public class UserDto
    {
        public UserDto()
        {
        }

        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
            UserName = user.UserName;
            Email = user.Email;
            try
            {
                UserCompany = user.Companies;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public String? Id { get; set; }
        public String? Name { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public ICollection<UserCompany> UserCompany { get; set; }
    }
}