using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Buildings
{
    [Table("Phases", Schema = "building")]
    public class Phase : BuildingBaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        public int BuildingId { get; set; }
        
        public Buildings.Building Building { get; set; }
        
        public List<WorkCategory> WorkCategories { get; set; }
        
        public decimal FullPrice { get; set; }
        
        public decimal? ExtraPrice { get; set; }
    }
}