using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Hr
{
    [Table("WorkerCategories", Schema = "hr")]
    public class WorkerCategory : BaseEntity
    {
        public string Name { get; set; }
    }
}