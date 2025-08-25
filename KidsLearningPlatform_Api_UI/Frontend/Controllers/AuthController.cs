using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AuthController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var apiUrl = _config["ApiBaseUrl"] + "/Token/login";
            var loginData = new { Email = email, Password = password };

            var content = new StringContent(
                JsonSerializer.Serialize(loginData),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid login!";
                return View();
            }

            var result = await response.Content.ReadAsStringAsync();
            var json = JsonDocument.Parse(result);
            var token = json.RootElement.GetProperty("token").GetString();

            HttpContext.Session.SetString("JWToken", token!);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(
            string userName,
            string email,
            string password,
            string role = "Parent"
        )
        {
            var apiUrl = _config["ApiBaseUrl"] + "/Token/register";
            var regData = new
            {
                UserName = userName,
                Email = email,
                Password = password,
                Role = role,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(regData),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Registration failed!";
                return View();
            }

            TempData["Success"] = "Registration successful. Please login.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
