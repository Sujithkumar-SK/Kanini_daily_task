using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public class Participant
{
  [Key]
  public int ParticipantId { get; set; }
  [Required, MaxLength(30)]
  public string? Name { get; set; }

  [Required, MaxLength(50), EmailAddress]
  public string? Email { get; set; }

  [Required]
  public string? Role { get; set; }

  [Required, MaxLength(10)]
  public string? PhoneNumber { get; set; }

  public ICollection<Session>? Sessions { get; set; }
}