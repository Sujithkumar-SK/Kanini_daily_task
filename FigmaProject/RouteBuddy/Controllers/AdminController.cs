using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteBuddy.Data;
using RouteBuddy.Models;

namespace RouteBuddy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RouteBuddyContext _ctx;
        public AdminController(RouteBuddyContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Dashboard()
        {
            var stats = new
            {
                Users = await _ctx.Users.CountAsync(),
                Vendors = await _ctx.Vendors.CountAsync(),
                Buses = await _ctx.Buses.CountAsync(),
                Bookings = await _ctx.Bookings.CountAsync(),
                Revenue = await _ctx.Payments.Where(p=>p.Status=="Success").SumAsync(p => (decimal?)p.Amount) ?? 0
            };
            return View(stats);
        }

        [HttpGet]
        public async Task<IActionResult> ManageBuses()
        {
            var buses = await _ctx.Buses.Include(b=>b.Vendor).ToListAsync();
            return View(buses);
        }

        [HttpPost]
        public async Task<IActionResult> AddBus(int vendorId, string busNumber, string busType, int seatCount)
        {
            _ctx.Buses.Add(new Bus{ VendorId = vendorId, BusNumber = busNumber, BusType = busType, SeatCount = seatCount });
            await _ctx.SaveChangesAsync();
            return RedirectToAction("ManageBuses");
        }
    }
}
