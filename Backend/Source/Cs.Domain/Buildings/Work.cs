using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Buildings
{
    [Table("Works", Schema = "building")]
    public class Work : BuildingBaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        public int WorkCategoryId { get; set; }
        
        public WorkCategory WorkCategory { get; set; }
        
        public int? HasToBeDoneAfterId { get; set; }
        
        public Work HasToBeDoneAfter { get; set; }
        
        public bool ExecuteByContractor { get; set; }
        
        public string ContractorName { get; set; }
        
        public decimal ContractorPrice { get; set; }
        
        public decimal? ContractorExtraPrice { get; set; }
        
        public List<WorkBuildingMaterial> BuildingMaterials { get; set; }
        public List<WorkConsumptionMaterial> ConsumptionMaterials { get; set; }
        public List<WorkMainMaterial> MainMaterials { get; set; }
        public List<WorkTechnic> Technics { get; set; }
        public List<WorkWorker> Workers { get; set; }
        public List<WorkBrigade> Brigades { get; set; }
        
        public decimal PriceForBMaterials { get; set; }
        public decimal PriceForCMaterials { get; set; }
        public decimal PriceForMMaterials { get; set; }
        public decimal PriceForTechnics { get; set; }
        public decimal PriceForBrigades { get; set; }
        public decimal PriceForWorkers { get; set; }
        
        public decimal? ExtraPricePercent { get; set; }
        
        [NotMapped]
        public decimal ResourcesPrice => PriceForBrigades + PriceForTechnics + PriceForWorkers +
                                         PriceForBMaterials + PriceForCMaterials + PriceForMMaterials;

        [NotMapped]
        public decimal Price => ExecuteByContractor
            ? ContractorPrice
            : ResourcesPrice;

        [NotMapped]
        public decimal? ExtraPrice => ExecuteByContractor
            ? ContractorExtraPrice
            : ResourcesPrice * ExtraPricePercent / 100;

        [NotMapped] public decimal FullPrice => Price + (ExtraPrice ?? 0);
        [NotMapped] public decimal ContractorFullPrice => ContractorPrice + (ContractorExtraPrice ?? 0);
        [NotMapped] public decimal ResourcesFullPrice => ResourcesPrice + ResourcesPrice * (ExtraPricePercent ?? 0) / 100;
    }
}