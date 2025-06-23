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

    public class CosmeticCreateViewModel
    {
        public CosmeticInformation Cosmetic { get; set; } = new();

        // Example for dropdown binding
        public List<CategoryOption> AvailableCategories { get; set; } = new();
    }

    public class CategoryOption
    {
        public string Value { get; set; } = "";
        public string Text { get; set; } = "";
    }

    public class CosmeticEditViewModel
    {
        public CosmeticInformation Cosmetic { get; set; } = new();
        public List<CategoryOption> AvailableCategories { get; set; } = new();
        public bool CanChangeCategory { get; set; } = true;
    }
}
