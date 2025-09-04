using Microsoft.EntityFrameworkCore;

public class ParticipantService : IParticipantService
{
  private readonly EventDbContext _context;
  public ParticipantService(EventDbContext context)
  {
    _context = context;
  }
  public async Task<Participant> RegisterParticipant(Participant data)
  {
    _context.Participants.Add(data);
    await _context.SaveChangesAsync();
    return (data);
  }
  public async Task<IEnumerable<Participant>> GetParticipantsBySession(int id)
  {
    var tmp = await _context.Participants.Where(s => s.Sessions!.Any(p => p.SessionId == id)).ToListAsync();
    return tmp;
  }
}