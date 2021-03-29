using System.Collections.Generic;
using Cs.Application.Files.Models;

namespace Cs.Application.Hr.Models
{
    public class WorkerViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Salary { get; set; }
        
        public decimal Coefficient { get; set; }
        
        public string Comment { get; set; }
        
        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public int PaymentTypeId { get; set; }
        
        public string PaymentTypeName { get; set; }
        
        public List<FileViewModel> Files { get; set; }
    }
}