using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WorkFlow.Shared.Entities
{
    public class User: IdentityUser
{
        public String? Name { get; set; }
        [NotMapped]
        public String Password { get; set; }
        [NotMapped]
        public String PasswordConfirm { get; set; }
        public UserCompany?[] Companies { get; set; }
    }
    
    public class UserCompany
    {
        [Key]
        public Guid Id { get; set; }
        public UserRole Role { get; set; } = UserRole.USER;
        public Company Company { get; set; }
        public Project?[] Projects { get; set; }
    }

    public class InviteUser
    {
        public string Email { get; set; }
    }
    
    public enum UserRole
    {
        USER,
        ADMIN
    }
}