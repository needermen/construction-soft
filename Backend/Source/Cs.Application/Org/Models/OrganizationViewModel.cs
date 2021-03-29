using System;

namespace Cs.Application.Org.Models
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }
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
        
        public string Logo { get; set; }
        
        public int[] RoleIds { get; set; }
    }
}