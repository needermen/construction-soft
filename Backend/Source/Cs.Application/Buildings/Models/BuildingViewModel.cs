using System;
using Cs.Domain.Buildings;

namespace Cs.Application.Buildings.Models
{
    public class BuildingViewModel : BuildingBaseViewModel
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
        public DateTime? AgreementDate { get; set; }
        public string Email { get; set; }
        public string Director { get; set; }
        public string DirectorPhoneNumber { get; set; }

        public BuildingStatus Status { get; set; } = BuildingStatus.ახალი;
        public string StatusName { get; set; }

        public decimal FullPrice { get; set; }
        public decimal? ExtraPrice { get; set; }
    }
}