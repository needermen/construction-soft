using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Materials;

namespace Cs.Domain.Buildings
{
    [Table("WorkMainMaterials", Schema = "building")]
    public class WorkMainMaterial
    {
        public int WorkId { get; set; }
        public int MaterialId { get; set; }
        
        public int Count { get; set; }
        
        public Work Work { get; set; }
        public MainMaterial Material { get; set; }
    }
}