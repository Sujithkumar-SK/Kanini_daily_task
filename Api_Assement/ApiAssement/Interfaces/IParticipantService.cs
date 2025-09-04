public interface IParticipantService
{
  public Task<Participant> RegisterParticipant(Participant participant);
  public Task<IEnumerable<Participant>> GetParticipantsBySession(int SessionId);
}