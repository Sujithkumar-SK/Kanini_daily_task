using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
    public async Task<IActionResult> Login(string username, string password)
    {
        var apiUrl = _config["ApiBaseUrl"] + "/Token";
        var loginData = new { username, password };

        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(apiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Invalid login!";
            return View();
        }

        var result = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(result);
        var token = json.RootElement.GetProperty("token").GetString();

        // Store token in Session
        HttpContext.Session.SetString("JWToken", token!);

        return RedirectToAction("Index", "Game");
    }
}
