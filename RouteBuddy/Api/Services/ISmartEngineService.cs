// Services/ISmartEngineService.cs
public interface ISmartEngineService
{
    Task<SearchResult?> FindBestRouteAsync(string start, string end, DateTime journeyDate, int maxHops = 3, TimeSpan? transferBuffer = null, CancellationToken ct = default);
}
