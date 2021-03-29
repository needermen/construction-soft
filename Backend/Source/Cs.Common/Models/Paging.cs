namespace Cs.Common.Models
{
    public class Paging
    {
        public int Page { get; set; }
        public int Count { get; set; } = 5;
        public string Search { get; set; }

        public int Skip => Page * Count;
    }
}