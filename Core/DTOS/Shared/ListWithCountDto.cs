using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOS.Shared
{
    public class ListWithCountDto<T> where T : class
    {
        public int Count { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
