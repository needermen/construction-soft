using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Technics
{
    [Table("TechnicCategories", Schema = "technic")]
    public class TechnicCategory : BaseEntity
    {
        public string Name { get; set; }
    }
}