using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Materials
{
    [Table("MainMaterialCategories", Schema = "material")]
    public class MainMaterialCategory : BaseEntity
    {
        public string Name { get; set; }
    }
}