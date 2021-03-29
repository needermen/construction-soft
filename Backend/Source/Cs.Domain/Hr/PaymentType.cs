using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Hr
{
    [Table("PaymentTypes", Schema = "hr")]
    public class PaymentType : BaseEntity
    {
        public string Name { get; set; }
    }
}