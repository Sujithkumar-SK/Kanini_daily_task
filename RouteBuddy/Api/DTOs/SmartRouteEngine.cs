// Models/SearchRequest.cs
public class SearchRequest
{
    public string Start { get; set; } = string.Empty;
    public string End { get; set; } = string.Empty;
    public DateTime JourneyDate { get; set; } // date part used
    public int MaxHops { get; set; } = 3; // 0 -> direct only, 1 -> allow one connection etc.
    public TimeSpan TransferBuffer { get; set; } = TimeSpan.FromMinutes(15);
}

// Models/RouteSegmentDto.cs
public class RouteSegmentDto
{
    public int ScheduleId { get; set; }
    public string BusName { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public decimal Fare { get; set; }
    public int SeatsAvailable { get; set; }
}

// Models/SearchResult.cs
public class SearchResult
{
    public List<RouteSegmentDto> Segments { get; set; } = new();
    public DateTime TotalDeparture => Segments.First().DepartureDateTime;
    public DateTime TotalArrival => Segments.Last().ArrivalDateTime;
    public int TotalHops => Segments.Count - 1;
    public decimal TotalFare => Segments.Sum(s => s.Fare);
}
