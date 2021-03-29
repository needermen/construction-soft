using System.ComponentModel.DataAnnotations.Schema;
using Cs.Domain.Hr;

namespace Cs.Domain.Files
{
    [Table("BrigadeFiles", Schema = "storage")]
    public class BrigadeFile
    {
        public int FileId { get; set; }
        public int BrigadeId { get; set; }
        
        public File File { get; set; }
        public Brigade Brigade { get; set; }
    }
}