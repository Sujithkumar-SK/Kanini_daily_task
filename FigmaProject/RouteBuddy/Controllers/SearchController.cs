using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteBuddy.Services;

namespace RouteBuddy.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly ISearchService _search;
        public SearchController(ISearchService search) => _search = search;

        [HttpGet]
        public IActionResult SearchBus() => View();

        [HttpPost]
        public async Task<IActionResult> Results(string source, string destination, DateTime travelDate)
        {
            var schedules = await _search.SearchAsync(source, destination, travelDate);
            return View(schedules);
        }
    }
}
