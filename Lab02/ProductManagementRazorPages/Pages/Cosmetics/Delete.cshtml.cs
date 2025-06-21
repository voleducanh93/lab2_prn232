using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ProductManagementRazorPages.Pages.Cosmetics
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _config;

        public DeleteModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Request.Cookies["token"];
            string baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await client.DeleteAsync($"{baseUrl}/api/CosmeticInformations/{Id}");

            return RedirectToPage("Index");
        }
    }
}
