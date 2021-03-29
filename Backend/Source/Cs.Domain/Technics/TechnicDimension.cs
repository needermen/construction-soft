using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Technics
{
    [Table("TechnicDimensions", Schema = "technic")]
    public class TechnicDimension : BaseEntity
    {
        public string Name { get; set; }
    }
}