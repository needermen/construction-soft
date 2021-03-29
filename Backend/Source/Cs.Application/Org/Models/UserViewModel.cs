namespace Cs.Application.Org.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalId { get; set; }
        public bool Active { get; set; } = true;
        
        public int[] RoleIds { get; set; }
        
        public int? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}