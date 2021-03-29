using System.Collections.Generic;

namespace Cs.Common.Models
{
    public class ListResult<T>
    {
        public int Total { get; set; }

        public List<T> Items { get; set; }
        
        public ListResult(List<T> items, int total)
        {
            Items = items;
            Total = total;
        }
    }
}