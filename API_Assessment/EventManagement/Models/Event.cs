using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public class Event
{
  [Key]
  public int EvetId { get; set; }

  [MaxLength(100), Required]
  public string? Title { get; set; }
  public string? Description { get; set; }
  public DateTime Date { get; set; }
  public string? Location { get; set; }
  
  public ICollection<Session>? Sessions { get; set; }
}