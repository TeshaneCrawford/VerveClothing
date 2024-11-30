using System.Collections.Generic;

namespace VerveClothingApi.Common
{
    /// <summary>
    /// Generic container for paginated data with navigation metadata.
    /// Use this class when implementing server-side pagination.
    /// </summary>
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // Calculated property: Ensures proper rounding up of total pages
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        // Navigation helpers for UI implementation
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        /// <summary>
        /// Creates a new paged result instance.
        /// Note: PageNumber is 1-based index for consistency with common UI patterns.
        /// </summary>
        public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}