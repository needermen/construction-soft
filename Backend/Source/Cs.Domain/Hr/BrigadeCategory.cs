using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Hr
{
    [Table("BrigadeCategories", Schema = "hr")]
    public class BrigadeCategory : BaseEntity
    {
        public string Name { get; set; }
    }
}