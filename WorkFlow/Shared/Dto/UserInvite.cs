using System;
using WorkFlow.Shared.Entities;

namespace WorkFlow.Shared.Dto {
    public class UserInvite {
        public String Email { get; set; }

        public Guid CompanyId { get; set; }

        public UserRole? Role { get; set; } = UserRole.User;
    }
}
