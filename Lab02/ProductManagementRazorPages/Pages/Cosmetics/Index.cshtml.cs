using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.ViewModel;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }
        public PagedCosmeticsViewModel ViewModel { get; set; } = new();
        public bool CanManage { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(string? searchTerm, int pageIndex = 1, int pageSize = 4)
        {
            int roleId = Convert.ToInt32(HttpContext.Request.Cookies["Role"]);
            CanManage = (roleId == 1);
            if (roleId == 2)
            {
                return RedirectToPage("/Unauthorized");
            }
            if (roleId < 1 || roleId > 4)
            {
                return RedirectToPage("/Unauthorized"); 
            }
            var token = HttpContext.Request.Cookies["token"];

            string baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{baseUrl}/api/CosmeticInformations");
            if (!response.IsSuccessStatusCode) return Page(); ;

            var json = await response.Content.ReadAsStringAsync();
            var Cosmetics = JsonSerializer.Deserialize<List<CosmeticInformation>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            // search
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                Cosmetics = Cosmetics
                    .Where(c => c.CosmeticName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) 
                    .ToList();
            }

            int totalCount = Cosmetics.Count;
            var paged = Cosmetics
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewModel = new PagedCosmeticsViewModel
            {
                Cosmetics = paged,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                SearchTerm = searchTerm
            };
            return Page();
        }

    }
}
