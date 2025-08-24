using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Frontend.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient _httpClient;

        public CourseController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("KidsLearningApi");
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Course");
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<CourseDto>()); // return empty list on failure
            }

            var json = await response.Content.ReadAsStringAsync();

            // ✅ Deserialize into Frontend.Models.CourseDto (NOT global CourseDto)
            var courses = JsonSerializer.Deserialize<List<Frontend.Models.CourseDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(courses);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Course/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<Frontend.Models.CourseDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(course);
        }
    }
}
