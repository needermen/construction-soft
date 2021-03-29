using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Materials;

namespace Cs.Domain.Buildings
{
    [Table("WorkBuildingMaterials", Schema = "building")]
    public class WorkBuildingMaterial
    {
        public int WorkId { get; set; }
        public int MaterialId { get; set; }
        
        public int Count { get; set; }
        
        public Work Work { get; set; }
        public BuildingMaterial BuildingMaterial { get; set; }
    }
}