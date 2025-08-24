using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using Frontend.Models;

public class CourseController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CourseController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("http://localhost:5225/api/Course");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<CourseDto>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var courses = JsonSerializer.Deserialize<List<CourseDto>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return View(courses);
    }
}

