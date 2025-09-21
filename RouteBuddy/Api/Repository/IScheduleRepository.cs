// Repositories/IScheduleRepository.cs
using System.Threading;
using System.Threading.Tasks;

public interface IScheduleRepository
{
    /// <summary>
    /// Get all schedules that depart on the journeyDate and have at least one seat available.
    /// </summary>
    Task<List<AvailableSchedule>> GetAvailableSchedulesAsync(DateTime journeyDate, CancellationToken ct = default);

    /// <summary>
    /// Optionally fetch schedules for specific source/destination on the date. Efficiency helper.
    /// </summary>
    Task<List<AvailableSchedule>> GetAvailableSchedulesForStopsAsync(string source, string destination, DateTime journeyDate, CancellationToken ct = default);
}
