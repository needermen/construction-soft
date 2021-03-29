using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Materials
{
    [Table("BuildingMaterialCategories", Schema = "material")]
    public class BuildingMaterialCategory : BaseEntity
    {
        public string Name { get; set; }
    }
}