using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Pagination_Response <TEntity> 
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity>Data { get; set; }
        public Pagination_Response(int pageIndex, int pageSize, int count,IEnumerable<TEntity>_Data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data= _Data;
        }

    }
}
