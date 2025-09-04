public interface ISessionService
{
  public Task<Session> CreateSession(Session data);
  public Task<IEnumerable<Session>> GetSessionByEventId(int id);
}