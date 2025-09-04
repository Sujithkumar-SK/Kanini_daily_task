using EventManagement.Models;
namespace EventManagement.Interfaces;

public interface IEventService
{
  Task<IEnumerable<Event>> GetAllEvents();
  Task<Event?> GetEventById(int id);
  Task<Event> CreateEvent(Event newEvent);
  Task<bool> UpdateEvent(int id, Event updatedEvent);
  Task<bool> DeleteEvent(int id);
}
