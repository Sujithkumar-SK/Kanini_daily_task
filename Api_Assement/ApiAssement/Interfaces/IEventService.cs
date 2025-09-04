public interface IEventService
{
  public Task<IEnumerable<Event>> GetAllEvents();
  public Task<Event> CreateEvent(Event ent);
  public Task<Event> UpdateEvent(int id, Event ent);
  public Task<Event> GetEventById(int id);
  public Task<bool> DeleteEvent(int id);
}