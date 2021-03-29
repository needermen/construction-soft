using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Hr;

namespace Cs.Domain.Buildings
{
    [Table("WorkBrigades", Schema = "building")]
    public class WorkBrigade
    {
        public int WorkId { get; set; }
        public int BrigadeId { get; set; }
        
        public int Count { get; set; }
        
        public Work Work { get; set; }
        public Brigade Brigade { get; set; }
    }
}