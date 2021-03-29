using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Buildings;
using Cs.Domain.Files;

namespace Cs.Domain.Hr
{
    [Table("Brigades", Schema = "hr")]
    public class Brigade : BaseEntity
    {
        public string Name { get; set; }
        
        public decimal Salary { get; set; }
        
        public int CategoryId { get; set; }
        
        public BrigadeCategory Category { get; set; }
        
        public int PaymentTypeId { get; set; }
        
        public PaymentType PaymentType { get; set; }
        
        public List<WorkBrigade> Works { get; set; }
        
        public List<BrigadeFile> Files { get; set; }
    }
}