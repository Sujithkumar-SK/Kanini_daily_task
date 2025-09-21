// Services/SmartEngineService.cs
using System.Collections.Immutable;

public class SmartEngineService : ISmartEngineService
{
    private readonly IScheduleRepository _repo;
    private readonly TimeSpan _defaultBuffer = TimeSpan.FromMinutes(15);
    private const int GLOBAL_MAX_HOPS = 5;

    public SmartEngineService(IScheduleRepository repo)
    {
        _repo = repo;
    }

    public async Task<SearchResult?> FindBestRouteAsync(string start, string end, DateTime journeyDate, int maxHops = 3, TimeSpan? transferBuffer = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end)) throw new ArgumentException("start and end required");
        if (start == end) throw new ArgumentException("start and end cannot be same");
        if (maxHops < 0) maxHops = 0;
        if (maxHops > GLOBAL_MAX_HOPS) maxHops = GLOBAL_MAX_HOPS;
        var buffer = transferBuffer ?? _defaultBuffer;

        // Load all relevant schedules for the date (can be optimized further by stop)
        var schedules = await _repo.GetAvailableSchedulesAsync(journeyDate, ct);
        if (ct.IsCancellationRequested) return null;

        // Convert schedules into lookup by source
        var bySource = schedules
            .Where(s => s.SeatsAvailable > 0)
            .GroupBy(s => s.Source)
            .ToDictionary(g => g.Key, g => g.OrderBy(s => s.DepartureDateTime).ToList());

        // BFS queue of paths (List<AvailableSchedule>)
        var queue = new Queue<List<AvailableSchedule>>();
        // seed with schedules starting at start
        if (bySource.TryGetValue(start, out var starters))
        {
            foreach (var s in starters)
            {
                queue.Enqueue(new List<AvailableSchedule> { s });
            }
        }
        // also try direct source->destination lookup optimization
        var bestPath = (List<AvailableSchedule>?)null;

        // visited control: we need to avoid reusing same schedule twice or cycles by stop+time
        while (queue.Count > 0)
        {
            if (ct.IsCancellationRequested) break;
            var path = queue.Dequeue();
            var last = path.Last();

            // prune by hops
            if (path.Count - 1 > maxHops) continue;

            // if reached destination
            if (last.Destination == end)
            {
                // prefer earliest arrival, then fewer hops, then lower fare
                if (IsBetterPath(path, bestPath))
                {
                    bestPath = path;
                }
                continue;
            }

            // If hops limit reached, don't expand further
            if (path.Count - 1 == maxHops) continue;

            // expand by schedules departing from last.Destination after last.Arrival + buffer
            if (!bySource.TryGetValue(last.Destination, out var nextCandidates)) continue;

            var earliestDeparture = last.ArrivalDateTime + buffer;
            foreach (var next in nextCandidates)
            {
                if (next.DepartureDateTime < earliestDeparture) continue; // not enough transfer time
                // avoid cycle on stops: if next.Destination already exists earlier in path stops, skip
                var stopsVisited = path.Select(p => p.Source).Append(path.Last().Destination).ToHashSet();
                if (stopsVisited.Contains(next.Destination) && next.Destination != end) continue; // avoid cycle unless it's the final destination
                // ensure we don't reuse exact schedule already in path
                if (path.Any(p => p.ScheduleId == next.ScheduleId)) continue;

                // seats check (already ensured in repo)
                if (next.SeatsAvailable <= 0) continue;

                // create new path and enqueue
                var newPath = new List<AvailableSchedule>(path) { next };
                queue.Enqueue(newPath);
            }
        }

        if (bestPath == null) return null;

        // Map to SearchResult
        var result = new SearchResult
        {
            Segments = bestPath.Select(bs => new RouteSegmentDto
            {
                ScheduleId = bs.ScheduleId,
                BusName = bs.BusName,
                Source = bs.Source,
                Destination = bs.Destination,
                DepartureDateTime = bs.DepartureDateTime,
                ArrivalDateTime = bs.ArrivalDateTime,
                Fare = bs.Fare,
                SeatsAvailable = bs.SeatsAvailable
            }).ToList()
        };

        return result;
    }

    private bool IsBetterPath(List<AvailableSchedule> candidate, List<AvailableSchedule>? currentBest)
    {
        if (currentBest == null) return true;
        var candArrival = candidate.Last().ArrivalDateTime;
        var bestArrival = currentBest.Last().ArrivalDateTime;
        if (candArrival < bestArrival) return true;
        if (candArrival > bestArrival) return false;
        // tie-break: fewer segments (less hops)
        if (candidate.Count < currentBest.Count) return true;
        if (candidate.Count > currentBest.Count) return false;
        // tie-break: lower total fare
        var candFare = candidate.Sum(x => x.Fare);
        var bestFare = currentBest.Sum(x => x.Fare);
        return candFare < bestFare;
    }
}
