using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Buildings;

namespace Cs.Domain.Materials
{
    [Table("MainMaterials", Schema = "material")]
    public class MainMaterial : BaseEntity
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal Depreciation { get; set; }
        
        public decimal Coefficient { get; set; }
        
        public string Comment { get; set; }
        
        [NotMapped] public string Description => $"{Name} ({Dimension?.Name})";
        
        public int CategoryId { get; set; }
        
        public MainMaterialCategory Category { get; set; }
        
        public int DimensionId { get; set; }
        
        public MaterialDimension Dimension { get; set; }
        
        public List<WorkMainMaterial> Works { get; set; }
    }
}