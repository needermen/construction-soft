namespace Cs.Application.Buildings.Models
{
    public class WorkViewModel : BuildingBaseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int WorkCategoryId { get; set; }

        public int? HasToBeDoneAfterId { get; set; }

        public string HasToBeDoneAfterName { get; set; }

        // contractor
        public bool ExecuteByContractor { get; set; }
        public string ContractorName { get; set; }
        public decimal ContractorPrice { get; set; }
        public decimal? ContractorExtraPrice { get; set; }

        // prices
        public decimal PriceForBMaterials { get; set; }
        public decimal PriceForCMaterials { get; set; }
        public decimal PriceForMMaterials { get; set; }
        public decimal PriceForTechnics { get; set; }
        public decimal PriceForBrigades { get; set; }
        public decimal PriceForWorkers { get; set; }

        public decimal? ExtraPricePercent { get; set; }

        public decimal ResourcesPrice { get; set; }

        public decimal Price { get; set; }
        public decimal? ExtraPrice { get; set; }
        public decimal FullPrice { get; set; }
        public decimal ContractorFullPrice { get; set; }
        public decimal ResourcesFullPrice { get; set; }
    }
}