using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Buildings;

namespace Cs.Domain.Technics
{
    [Table("Technics", Schema = "technic")]
    public class Technic : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal Coefficient { get; set; }
        public string Comment { get; set; }
        
        [NotMapped] public string Description => $"{Name} ({Dimension?.Name})";

        public int CategoryId { get; set; }
        public TechnicCategory Category { get; set; }

        public int DimensionId { get; set; }
        public TechnicDimension Dimension { get; set; }

        public List<WorkTechnic> Works { get; set; }
    }
}