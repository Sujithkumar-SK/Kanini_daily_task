using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteBuddy.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string Role { get; set; } = "User"; // User, Vendor, Admin
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public Vendor? VendorProfile { get; set; } // if user is a vendor
    }

    public class Vendor
    {
        public int VendorId { get; set; }
        public int UserId { get; set; } // owner
        [Required, MaxLength(100)]
        public string VendorName { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? ContactInfo { get; set; }

        public User? User { get; set; }
        public ICollection<Bus>? Buses { get; set; }
    }

    public class Bus
    {
        public int BusId { get; set; }
        public int VendorId { get; set; }
        [Required, MaxLength(30)]
        public string BusNumber { get; set; } = string.Empty;
        [MaxLength(30)]
        public string? BusType { get; set; }
        [Range(1, 120)]
        public int SeatCount { get; set; } = 40;

        public Vendor? Vendor { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }

    public class Route
    {
        public int RouteId { get; set; }
        [Required, MaxLength(60)]
        public string Source { get; set; } = string.Empty;
        [Required, MaxLength(60)]
        public string Destination { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
        public TimeSpan Duration { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }

    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        [Range(0, 100000)]
        public decimal Price { get; set; }

        public Bus? Bus { get; set; }
        public Route? Route { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<SeatHold>? SeatHolds { get; set; }
    }

    public class Booking
    {
        public int BookingId { get; set; }
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        [Required, MaxLength(12)]
        public string PNR { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending/Confirmed/Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Schedule? Schedule { get; set; }
        public User? User { get; set; }
        public Payment? Payment { get; set; }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        [MaxLength(20)]
        public string Method { get; set; } = "Card"; // Card/UPI/Wallet
        [MaxLength(20)]
        public string Status { get; set; } = "Success"; // Success/Failed

        public Booking? Booking { get; set; }
        public User? User { get; set; }
    }

    // Seat hold to prevent double booking while paying
    public class SeatHold
    {
        public int SeatHoldId { get; set; }
        public int ScheduleId { get; set; }
        public int SeatNumber { get; set; }
        public DateTime ExpiresAt { get; set; } // release after this time
        [MaxLength(64)]
        public string Token { get; set; } = string.Empty; // unique token per hold

        public Schedule? Schedule { get; set; }
    }
}
