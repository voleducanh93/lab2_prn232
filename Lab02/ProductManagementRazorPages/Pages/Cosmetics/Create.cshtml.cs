using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.ViewModel;
using BusinessObjects.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _config;

        public CreateModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public CosmeticCreateViewModel ViewModel { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            ViewModel.AvailableCategories = await LoadCategories();
            return Partial("_CreatePartial", ViewModel); 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewModel.AvailableCategories = await LoadCategories();
                return Partial("_CreatePartial", ViewModel);
            }

            var token = HttpContext.Request.Cookies["token"];
            var baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(ViewModel.Cosmetic), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{baseUrl}/api/CosmeticInformations", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to create cosmetic.");
                ViewModel.AvailableCategories = await LoadCategories();
                return Partial("_CreatePartial", ViewModel);
            }

            return new JsonResult(new { success = true });
        }


        private async Task<List<CategoryOption>> LoadCategories()
        {
            var token = HttpContext.Request.Cookies["token"];
            var baseUrl = _config["ApiBaseUrl"];
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{baseUrl}/api/CosmeticCategories");
            if (!response.IsSuccessStatusCode) return new();

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CosmeticCategory>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

            return categories.Select(c => new CategoryOption
            {
                Value = c.CategoryId,
                Text = c.CategoryName
            }).ToList();
        }
    }
}
