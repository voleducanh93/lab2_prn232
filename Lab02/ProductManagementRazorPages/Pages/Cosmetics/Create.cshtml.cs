using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
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
        public CosmeticInput Cosmetic { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Request.Cookies["token"];
            string baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(Cosmetic);
            var response = await client.PostAsync($"{baseUrl}/api/CosmeticInformations",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ErrorMessage = "Failed to create product. Please check your input or permissions.";
            return Page();
        }

        public class CosmeticInput
        {
            public string CosmeticName { get; set; }
            public string SkinType { get; set; }
            public string ExpirationDate { get; set; }
            public string CosmeticSize { get; set; }
            public decimal DollarPrice { get; set; }
            public string CategoryId { get; set; }
        }
    }
}
