using System;
namespace booking
{
  class Program
  {
    static void Main(string[] args)
    {
      BookingSystem<Bus> bus = new BookingSystem<Bus>();

      bus.AddBooking(new Bus
      {
        BusId = 1,
        PassengerName = "Sujith",
        BusName = "KPN Travel",
        Source = "Chennai",
        Destination = "Coimabatore"
      });

      bus.AddBooking(new Bus
      {
        BusId = 2,
        PassengerName = "Arun",
        BusName = "SRS Travel",
        Source = "Banglore",
        Destination = "Madurai"
      });

      Console.WriteLine("Bus Bookings:");
      foreach (var booking in bus.Display())
      {
        Console.WriteLine($"ID: {booking.BusId}, Name: {booking.PassengerName}, Bus: {booking.BusName}, From: {booking.Source}, To: {booking.Destination}");
      }
    }
  }
}
