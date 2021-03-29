using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Buildings
{
    [Table("WorkCategories", Schema = "building")]
    public class WorkCategory : BuildingBaseEntity
    {
        public string Name { get; set; }
        
        public int PhaseId { get; set; }
        
        public Phase Phase { get; set; }
        
        public List<Work> Works { get; set; }
        
        public decimal FullPrice { get; set; }
        
        public decimal? ExtraPrice { get; set; }
    }
}