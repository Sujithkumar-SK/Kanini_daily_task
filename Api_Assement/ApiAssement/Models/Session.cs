using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Session
{
  [Key]
  public int SessionId { get; set; }

  public string? Title { get; set; }
  public string? Speaker { get; set; }
  public DateTime StartTime { get; set; }
  public DateTime EndTime { get; set; }

  [ForeignKey("Event")]
  public int EventId { get; set; }
  
  public Event? events { get; set; }
  
  public ICollection<Participant>? Participants { get; set; }
}