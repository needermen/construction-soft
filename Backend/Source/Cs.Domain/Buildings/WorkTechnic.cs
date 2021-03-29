using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Technics;

namespace Cs.Domain.Buildings
{
    [Table("WorkTechnics", Schema = "building")]
    public class WorkTechnic
    {
        public int WorkId { get; set; }
        public int TechnicId { get; set; }
        
        public int Count { get; set; }
        
        public Work Work { get; set; }
        public Technic Technic { get; set; }
    }
}