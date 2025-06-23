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

        public async Task<IActionResult> OnPostAsync([FromForm] string id)
        {
            var token = HttpContext.Request.Cookies["token"];
            var baseUrl = _config["ApiBaseUrl"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"{baseUrl}/api/CosmeticInformations/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = false });
            }

            return new JsonResult(new { success = true });
        }
    }
}
