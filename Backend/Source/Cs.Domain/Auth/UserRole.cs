using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Auth
{
    [Table("UserRoles", Schema = "auth")]
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
        public User User { get; set; }
        
        public Role Role { get; set; }
    }
}