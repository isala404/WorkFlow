using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow.Shared.Entities
{
    public class UserCompany
    {
        [Key]
        public Guid UserCompanyId { get; set; }
        
        [Required]
        public String UserId { get; set; }
        
        [Required]
        public Guid CompanyId { get; set; }
        
        [Required]
        public UserRole Role { get; set; } = UserRole.User;
        
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}