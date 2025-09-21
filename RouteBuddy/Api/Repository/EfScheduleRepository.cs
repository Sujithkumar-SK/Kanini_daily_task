using Microsoft.EntityFrameworkCore;

public class EfScheduleRepository : IScheduleRepository
{
  private readonly BackendDbContext _ctx;
  public EfScheduleRepository(BackendDbContext ctx) => _ctx = ctx;

  public async Task<List<AvailableSchedule>> GetAvailableSchedulesAsync(DateTime journeyDate, CancellationToken ct = default)
  {
    var date = journeyDate.Date;

    // Load schedules for that date and compute seats available by counting BookedSeats
    var schedules = await _ctx.BusSchedules
        .Include(s => s.Bus)
        .Include(s => s.Route)
        .Where(s => s.TravelDate.Date == date && s.Status == "Scheduled")
        .Select(s => new
        {
          s.ScheduleId,
          s.DepartureTime,
          s.ArrivalTime,
          BusId = s.BusId,
          BusName = s.Bus.BusName,
          Source = s.Route.Source,
          Destination = s.Route.Destination,
          s.RouteId,
          s.TravelDate
        })
        .ToListAsync(ct);

    // Precompute booked counts per schedule
    var bookedCounts = await _ctx.BookedSeats
        .Include(bs => bs.BookingSegment)
        .Where(bs => bs.BookingSegment.Schedule != null && bs.BookingSegment.Schedule.TravelDate.Date == date)
        .GroupBy(bs => bs.BookingSegment.ScheduleId)
        .Select(g => new { ScheduleId = g.Key, Count = g.Count() })
        .ToListAsync(ct);

    var bookedMap = bookedCounts.ToDictionary(x => x.ScheduleId, x => x.Count);

    // Fetch fares per schedule (take minimum fare if multiple seat types)
    var fares = await _ctx.Fares
        .GroupBy(f => f.ScheduleId)
        .Select(g => new { ScheduleId = g.Key, MinFare = g.Min(x => x.Price) })
        .ToListAsync(ct);

    var fareMap = fares.ToDictionary(x => x.ScheduleId, x => x.MinFare);

    var result = new List<AvailableSchedule>(schedules.Count);

    foreach (var s in schedules)
    {
      // get bus total seats
      var totalSeats = await _ctx.Buses
          .Where(b => b.BusId == s.BusId)
          .Select(b => b.TotalSeats)
          .FirstOrDefaultAsync(ct);

      int booked = bookedMap.TryGetValue(s.ScheduleId, out var c) ? c : 0;
      int seatsAvailable = Math.Max(0, totalSeats - booked);

      result.Add(new AvailableSchedule
      {
        ScheduleId = s.ScheduleId,
        Source = s.Source,
        Destination = s.Destination,

        // ✅ Combine TravelDate with TimeSpan correctly
        DepartureDateTime = s.TravelDate.Date.Add(s.DepartureTime),
        ArrivalDateTime = s.TravelDate.Date.Add(s.ArrivalTime),

        BusId = s.BusId,
        BusName = s.BusName,
        SeatsAvailable = seatsAvailable,
        Fare = fareMap.TryGetValue(s.ScheduleId, out var f) ? f : 0m
      });
    }

    return result;
  }

  public async Task<List<AvailableSchedule>> GetAvailableSchedulesForStopsAsync(
      string source,
      string destination,
      DateTime journeyDate,
      CancellationToken ct = default)
  {
    var all = await GetAvailableSchedulesAsync(journeyDate, ct);
    return all
        .Where(s => s.Source == source && s.Destination == destination && s.SeatsAvailable > 0)
        .ToList();
  }
}
