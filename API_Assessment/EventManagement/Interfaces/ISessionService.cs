using EventManagement.Models;

public interface ISessionService
{
    Task<Session> CreateSession(Session session);
    Task<IEnumerable<Session>> GetSessionsByEvent(int eventId);
}