using Radium.Shared.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Radium.Shared.Utils.Requests
{
    public class PaginationFilterRequest
    {
        public PaginationFilterRequest()
        {
            PageNumber = 1;
            PageSize = 12;
        }

        public PaginationFilterRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1: pageNumber;
            PageSize = pageSize > 1000 ? 1000 : pageSize;
        }

        public PaginationFilterRequest(int pageNumber, int pageSize, string search) : this(pageNumber, pageSize)
        {
            Search = search;
        }
        public PaginationFilterRequest(int pageNumber, int pageSize, string search, SortBy sortBy) : this(pageNumber, pageSize)
        {
            Search = search;
            Sort = sortBy;
        }


        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public List<int>? Filter { get; set; }
        public SortBy Sort { get; set; }
    }
}
