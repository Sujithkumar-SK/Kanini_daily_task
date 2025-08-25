using System.Net.Http.Headers;
using System.Text.Json;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UsersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Users
        public async Task<IActionResult> Index()
        {
            // 🔑 Get JWT from session
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // 🔗 Call your backend API
            var response = await client.GetAsync("http://localhost:5225/api/Users");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<UserDto>()); // return empty list on failure
            }

            var json = await response.Content.ReadAsStringAsync();

            // ✅ Deserialize into UserDto list
            var users = JsonSerializer.Deserialize<List<UserDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(users);
        }
    }
}
