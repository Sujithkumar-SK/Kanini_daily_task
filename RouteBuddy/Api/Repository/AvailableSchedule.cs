// Repositories/AvailableSchedule.cs
public class AvailableSchedule
{
    public int ScheduleId { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime { get; set; }
    public int BusId { get; set; }
    public string BusName { get; set; } = string.Empty;
    public int SeatsAvailable { get; set; }
    public decimal Fare { get; set; }
}
