using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Auth
{
    [Table("OrganizationRoles", Schema = "org")]
    public class OrganizationRole
    {
        public int OrganizationId { get; set; }
        public int RoleId { get; set; }
        
        public Organization Organization { get; set; }
        
        public Role Role { get; set; }
    }
}