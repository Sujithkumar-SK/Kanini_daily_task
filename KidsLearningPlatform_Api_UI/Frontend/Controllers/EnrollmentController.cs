using System.Net.Http.Headers;
using System.Text.Json;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;

public class EnrollmentController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EnrollmentController(IHttpClientFactory httpClientFactory)
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
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("http://localhost:5225/api/Enrollment");
        if (!response.IsSuccessStatusCode)
        {
            return View(new List<EnrollmentDto>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var enrollments = JsonSerializer.Deserialize<List<EnrollmentDto>>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return View(enrollments);
    }

    // Create Enrollment (GET)
    public async Task<IActionResult> Create()
    {
        // Fetch kids and courses to populate dropdowns
        var client = _httpClientFactory.CreateClient();
        var token = HttpContext.Session.GetString("JWToken");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var kidsResponse = await client.GetAsync("http://localhost:5225/api/Kid");
        var coursesResponse = await client.GetAsync("http://localhost:5225/api/Course");

        var kids = new List<KidsDto>();
        var courses = new List<CourseDto>();

        if (kidsResponse.IsSuccessStatusCode)
        {
            var kidsJson = await kidsResponse.Content.ReadAsStringAsync();
            kids = JsonSerializer.Deserialize<List<KidsDto>>(
                kidsJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        if (coursesResponse.IsSuccessStatusCode)
        {
            var coursesJson = await coursesResponse.Content.ReadAsStringAsync();
            courses = JsonSerializer.Deserialize<List<CourseDto>>(
                coursesJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }

        ViewBag.Kids = kids;
        ViewBag.Courses = courses;

        return View();
    }

    // Create Enrollment (POST)
    [HttpPost]
    public async Task<IActionResult> Create(EnrollmentDto enrollment)
    {
        var client = _httpClientFactory.CreateClient();
        var token = HttpContext.Session.GetString("JWToken");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync(
            "http://localhost:5225/api/Enrollment",
            enrollment
        );

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Failed to enroll kid in course.");
        return View(enrollment);
    }

    // Delete Enrollment
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var token = HttpContext.Session.GetString("JWToken");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.DeleteAsync($"http://localhost:5225/api/Enrollment/{id}");

        return RedirectToAction("Index");
    }
}
