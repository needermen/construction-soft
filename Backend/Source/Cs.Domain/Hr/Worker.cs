using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Buildings;
using Cs.Domain.Files;

namespace Cs.Domain.Hr
{
    [Table("Workers", Schema = "hr")]
    public class Worker : BaseEntity
    {
        public string Name { get; set; }
        
        public decimal Salary { get; set; }
        
        public decimal Coefficient { get; set; }
        
        public string Comment { get; set; }
        
        public int CategoryId { get; set; }
        
        public WorkerCategory Category { get; set; }
        
        public int PaymentTypeId { get; set; }
        
        public PaymentType PaymentType { get; set; }
        
        public List<WorkWorker> Works { get; set; }
        
        public List<WorkerFile> Files { get; set; }
    }
}