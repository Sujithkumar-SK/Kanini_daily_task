using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteBuddy.Services;
using System.Security.Claims;

namespace RouteBuddy.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _booking;
        public BookingController(IBookingService booking) => _booking = booking;

        [HttpGet]
        public IActionResult BookSeat(int scheduleId) { ViewBag.ScheduleId = scheduleId; return View(); }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(int scheduleId, int seatNumber)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _booking.HoldSeatAndCreateBookingAsync(scheduleId, userId, seatNumber);
            if (!result.ok) { ViewBag.Error = result.message; return View("BookSeat", scheduleId); }
            ViewBag.BookingId = result.booking!.BookingId;
            return View(result.booking);
        }
    }
}
