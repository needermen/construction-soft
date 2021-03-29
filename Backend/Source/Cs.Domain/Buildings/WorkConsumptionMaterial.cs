using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Materials;

namespace Cs.Domain.Buildings
{
    [Table("WorkConsumptionMaterials", Schema = "building")]
    public class WorkConsumptionMaterial
    {
        public int WorkId { get; set; }
        public int MaterialId { get; set; }
        
        public int Count { get; set; }
        
        public Work Work { get; set; }
        public ConsumptionMaterial Material { get; set; }
    }
}