using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

public class SessionService : ISessionService
{
  private readonly EventDbContext _context;
  public SessionService(EventDbContext context)
  {
    _context = context;
  }

  public async Task<Session> CreateSession(Session session)
  {
    _context.Sessions.Add(session);
    await _context.SaveChangesAsync();
    return session;
  }

  public async Task<IEnumerable<Session>> GetSessionsByEvent(int eventId)
  {
    return await _context.Sessions
                        .Where(s => s.EventId == eventId)
                        .ToListAsync();
  }
}