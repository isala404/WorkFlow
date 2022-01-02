using System;
using System.Collections.Generic;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto {
    public class UserDto {
        public UserDto() {}
        public UserDto(User user, Boolean expand = true) {
            Id = user.Id;
            Name = user.Name;
            UserName = user.UserName;
            Email = user.Email;

            if (!expand) return;

            if (user.Companies == null) return;
            foreach (var userCompany in user.Companies) UserCompany?.Add(new UserCompanyDto(userCompany));
        }

        public String? Id { get; set; }

        public String? Name { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public String? Password { get; set; }

        public ICollection<UserCompanyDto>? UserCompany { get; set; } = new List<UserCompanyDto>();
    }
}
