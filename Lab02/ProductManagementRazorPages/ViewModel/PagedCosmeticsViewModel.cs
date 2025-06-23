using BusinessObjects.Models;

namespace ProductManagementRazorPages.ViewModel
{
    public class PagedCosmeticsViewModel
    {
        public List<CosmeticInformation> Cosmetics { get; set; } = new();
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalCount { get; set; }
        public string? SearchTerm { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
