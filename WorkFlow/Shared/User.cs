using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow.Shared
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String PasswordHash { get; set; }
        [NotMapped]
        public String Password { get; set; }
        [NotMapped]
        public String PasswordConfirm { get; set; }
        public Project[] Projects { get; set; }
        public Company[] Companies { get; set; }
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