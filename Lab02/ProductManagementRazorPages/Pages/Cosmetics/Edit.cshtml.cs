using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductManagementRazorPages.Pages.Cosmetics
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _config;

        public EditModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public CosmeticInput Cosmetic { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Request.Cookies["token"];
            string baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{baseUrl}/api/CosmeticInformations/{Id}");
            if (!response.IsSuccessStatusCode) return RedirectToPage("Index");

            var json = await response.Content.ReadAsStringAsync();
            Cosmetic = JsonSerializer.Deserialize<CosmeticInput>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Request.Cookies["token"];
            string baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(Cosmetic);
            var response = await client.PutAsync($"{baseUrl}/api/CosmeticInformations/{Id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            return RedirectToPage("Index");
        }

        public class CosmeticInput
        {
            public string CosmeticId { get; set; }
            public string CosmeticName { get; set; }
            public string SkinType { get; set; }
            public string ExpirationDate { get; set; }
            public string CosmeticSize { get; set; }
            public decimal DollarPrice { get; set; }
            public string CategoryId { get; set; }
        }
    }
}
