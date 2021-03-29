using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Hr;

namespace Cs.Domain.Buildings
{
    [Table("WorkWorkers", Schema = "building")]
    public class WorkWorker
    {
        public int WorkId { get; set; }
        public int WorkerId { get; set; }
        
        public int Count { get; set; }
        
        public Work Work { get; set; }
        public Worker Worker { get; set; }
    }
}