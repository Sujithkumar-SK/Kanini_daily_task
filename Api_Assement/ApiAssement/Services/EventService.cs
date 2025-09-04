using Microsoft.EntityFrameworkCore;

public class EventService : IEventService
{
  private readonly EventDbContext _context;
  public EventService(EventDbContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<Event>> GetAllEvents()
  {
    return await _context.Events.ToListAsync();
  }
  public async Task<Event> GetEventById(int id)
  {
    return await _context.Events.Include(s => s.Sessions).FirstOrDefaultAsync(s => s.EventId == id);
  }
  public async Task<Event> UpdateEvent(int id, Event data)
  {
    var tmp = await _context.Events.FindAsync(id);
    if (tmp == null) return null;
    tmp.Tilte = data.Tilte;
    tmp.Description = data.Description;
    tmp.Location = data.Location;
    tmp.Date = data.Date;
    await _context.SaveChangesAsync();
    return tmp;
  }
  public async Task<Event> CreateEvent(Event data)
  {
    _context.Events.Add(data);
    await _context.SaveChangesAsync();
    return data;
  }
  public async Task<bool> DeleteEvent(int id)
  {
    var tmp = await _context.Events.FindAsync(id);
    if (tmp == null) return false;
    _context.Events.Remove(tmp);
    await _context.SaveChangesAsync();
    return true;
  }
}