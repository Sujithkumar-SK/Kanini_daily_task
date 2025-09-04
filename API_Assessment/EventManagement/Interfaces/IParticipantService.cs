using EventManagement.Models;

public interface IParticipantService
{
    Task<Participant> RegisterParticipant(Participant participant);
    Task<IEnumerable<Participant>> GetParticipantsBySession(int sessionId);
}