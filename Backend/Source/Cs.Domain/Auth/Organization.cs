using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Auth
{
    [Table("Organizations", Schema = "org")]
    public class Organization : BaseEntity
    {
        public string Name { get; set; }
        public string TaxCode { get; set; }
        public string PhoneNumber { get; set; }
        public string LegalAddress { get; set; }
        public string ActualAddress { get; set; }
        public string Bank { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime AgreementDate { get; set; }
        public string Director { get; set; }
        public string DirectorPhoneNumber { get; set; }
        public bool Active { get; set; } = true;
        public byte[] Logo { get; set; }
        
        public List<User> Users { get; set; }
        public List<OrganizationRole> Roles { get; set; }
    }
}