using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Cs.Domain.Auth
{
    [Table("Roles", Schema = "auth")]
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        
        [JsonIgnore]
        public List<UserRole> Users { get; set; }
        
        [JsonIgnore]
        public List<OrganizationRole> OrganizationRoles { get; set; }
    }

    public enum RoleEnums
    {
        Admin = 1,
        ResourceManager = 2,
        CostEstimator = 3
    }
}