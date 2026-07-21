using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Common.Pagination
{
    public class PaginationParams

    {
        public string? SortBy { get; set; }

        public bool Descending { get; set; } = false;
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public string? Search { get; set; }
        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }
        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = value > MaxPageSize
                ? MaxPageSize
                : value;
        }
    }
}
