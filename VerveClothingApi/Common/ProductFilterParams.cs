namespace VerveClothingApi.Common
{
    public class ProductFilterParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; }
        public bool SortDesc { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Category { get; set; }
        public bool? IsActive { get; set; }
    }
}