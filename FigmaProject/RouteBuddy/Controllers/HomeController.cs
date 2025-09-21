using Microsoft.AspNetCore.Mvc;

namespace RouteBuddy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
