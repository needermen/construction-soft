using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Hr;

namespace Cs.Domain.Files
{
    [Table("WorkerFiles", Schema = "storage")]
    public class WorkerFile
    {
        public int FileId { get; set; }
        public int WorkerId { get; set; }
        
        public File File { get; set; }
        public Worker Worker { get; set; }
    }
}