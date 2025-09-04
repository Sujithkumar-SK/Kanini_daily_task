using Microsoft.EntityFrameworkCore;

public class SessionService : ISessionService
{
  private readonly EventDbContext _context;
  public SessionService(EventDbContext context)
  {
    _context = context;
  }
  public async Task<Session> CreateSession(Session data)
  {
    _context.Sessions.Add(data);
    await _context.SaveChangesAsync();
    return data;
  }
  public async Task<IEnumerable<Session>> GetSessionByEventId(int id)
  {
    var tmp = await _context.Sessions.Where(s => s.EventId == id).ToListAsync();
    return tmp;
  }
}