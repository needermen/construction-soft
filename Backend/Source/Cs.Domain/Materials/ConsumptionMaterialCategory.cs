using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Materials
{
    [Table("ConsumptionMaterialCategories", Schema = "material")]
    public class ConsumptionMaterialCategory : BaseEntity
    {
        public string Name { get; set; }
    }
}