using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Buildings;

namespace Cs.Domain.Materials
{
    [Table("ConsumptionMaterials", Schema = "material")]
    public class ConsumptionMaterial : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
        
        public decimal Coefficient { get; set; }
        
        public string Comment { get; set; }
        
        [NotMapped] public string Description => $"{Name} ({Dimension?.Name})";

        public int CategoryId { get; set; }

        public ConsumptionMaterialCategory Category { get; set; }

        public int DimensionId { get; set; }

        public MaterialDimension Dimension { get; set; }

        public List<WorkConsumptionMaterial> Works { get; set; }
    }
}