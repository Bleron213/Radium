using System;
using System.Collections.Generic;
using System.Text;

namespace Radium.Shared.Utils.Responses
{
    public class PagedResponse<T> where T : class
    {
        public PagedResponse(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = totalCount > 0 ? (int) Math.Ceiling(totalCount / (double) pageSize) : 0;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
