using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Auth
{
    [Table("Users", Schema = "auth")]
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalId { get; set; }
        public bool Active { get; set; } = true;
        
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool PasswordShouldChange { get; set; } = true;
        
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }

        public List<UserRole> Roles { get; set; }
        
        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}