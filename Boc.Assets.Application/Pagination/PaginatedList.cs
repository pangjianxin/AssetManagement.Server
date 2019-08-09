
using Boc.Assets.Application.Sieve.Models;
using System;
using System.Collections.Generic;

namespace Boc.Assets.Application.Pagination
{
    public class PaginatedList<TEntity> : List<TEntity> where TEntity : class
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public int TotalItemsCount { get; set; }

        public int PageCount => (int)Math.Ceiling((double)TotalItemsCount / PageSize);
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < PageCount;
        public PaginatedList(SieveOptions option, int? pageIndex, int? pageSize, int totalItemsCount, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex ?? 1;
            PageSize = pageSize ?? option.DefaultPageSize;
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }
    }
}