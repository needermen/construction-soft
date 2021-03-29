using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cs.Domain.Files
{
    [Table("Files", Schema = "storage")]
    public class File : BaseEntity
    {
        public byte[] Content { get; set; }
        public string Filename { get; set; }
        public string Format { get; set; }
        
        public List<BrigadeFile> Brigades { get; set; }
        public List<WorkerFile> Workers { get; set; }
    }
}