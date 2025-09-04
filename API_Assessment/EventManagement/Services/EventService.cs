using EventManagement.Interfaces;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Services;

public class EventService : IEventService
{
  private readonly EventDbContext _context;
  public EventService(EventDbContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<Event>> GetAllEvents()
  {
    return await _context.Events.ToListAsync();
  }
  public async Task<Event?> GetEventById(int id)
  {
    return await _context.Events.FirstOrDefaultAsync(s => s.EvetId == id);
  }
  public async Task<Event> CreateEvent(Event newEvent)
  {
    _context.Events.Add(newEvent);
    await _context.SaveChangesAsync();
    return newEvent;
  }
  public async Task<bool> UpdateEvent(int id, Event updatedEvent)
  {
    var temp = await _context.Events.FindAsync(id);
    if (temp == null)
    {
      return false;
    }
    temp.Title = updatedEvent.Title;
    temp.Description = updatedEvent.Description;
    temp.Date = updatedEvent.Date;
    temp.Location = updatedEvent.Location;

    _context.Events.Update(temp);
    await _context.SaveChangesAsync();
    return true;
  }
  public async Task<bool> DeleteEvent(int id)
  {
    var temp = await _context.Events.FindAsync(id);
    if (temp == null)
    {
      return false;
    }
    _context.Events.Remove(temp);
    await _context.SaveChangesAsync();
    return true;
  }
}