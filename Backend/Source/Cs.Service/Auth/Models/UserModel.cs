using System;

namespace Cs.Service.Auth.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public int[] RoleIds { get; set; }
        public int? OrganizationId { get; set; }
        public bool PasswordShouldChange { get; set; }
    }
}