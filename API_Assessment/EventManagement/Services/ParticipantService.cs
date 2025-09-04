using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

public class ParticipantService : IParticipantService
{
  private readonly EventDbContext _context;
  public ParticipantService(EventDbContext context)
  {
    _context = context;
  }

  public async Task<Participant> RegisterParticipant(Participant participant)
  {
    _context.Participants.Add(participant);
    await _context.SaveChangesAsync();
    return participant;
  }

  public async Task<IEnumerable<Participant>> GetParticipantsBySession(int sessionId)
  {
    return await _context.Participants
    .Where(p => p.Sessions.Any(s => s.SessionId == sessionId))
    .ToListAsync();
  }
}