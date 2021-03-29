using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Materials
{
    [Table("MaterialDimensions", Schema = "material")]
    public class MaterialDimension : BaseEntity
    {
        public string Name { get; set; }
    }
}