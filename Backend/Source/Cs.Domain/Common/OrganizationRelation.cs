using Cs.Domain.Auth;

namespace Cs.Domain.Common
{
    public class OrganizationRelation : BaseEntity
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}