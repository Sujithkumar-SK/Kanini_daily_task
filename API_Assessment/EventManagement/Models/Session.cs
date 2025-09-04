using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagement.Models;

public class Session
{
  [Key]
  public int SessionId { get; set; }

  [MaxLength(100), Required]

  public string? Title { get; set; }

  [MaxLength(100), Required]
  public String? Speaker { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }

  [ForeignKey("Event")]
  public int EventId { get; set; }
  public Event? Event { get; set; }

  public ICollection<Participant>? Participants { get; set; }

}