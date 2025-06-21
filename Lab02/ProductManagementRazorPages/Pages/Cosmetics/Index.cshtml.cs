using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
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

        public List<CosmeticInformation> Cosmetics { get; set; } = new();

        public async Task OnGetAsync()
        {
            var token = HttpContext.Request.Cookies["token"];
            string baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{baseUrl}/api/CosmeticInformations");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Cosmetics = JsonSerializer.Deserialize<List<CosmeticInformation>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            }
        }

        public class CosmeticInformation
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
