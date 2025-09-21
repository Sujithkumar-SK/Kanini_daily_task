using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteBuddy.Services;
using System.Security.Claims;

namespace RouteBuddy.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _payment;
        public PaymentController(IPaymentService payment) => _payment = payment;

        [HttpGet]
        public IActionResult Pay(int bookingId) { ViewBag.BookingId = bookingId; return View(); }

        [HttpPost]
        public async Task<IActionResult> Pay(int bookingId, string method)
        {
            int userId = int.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)!);
            var (ok, message) = await _payment.PayAsync(bookingId, userId, method);
            ViewBag.Message = message;
            return View(ok ? "Success" : "Failure");
        }
    }
}
