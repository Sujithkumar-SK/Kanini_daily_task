using System.ComponentModel.DataAnnotations;

public class Participant
{
  [Key]
  public int ParticipantId { get; set; }

  public string? Name { get; set; }

  public string? Email { get; set; }

  public string? Phone { get; set; }
  
  public ICollection<Session>?Sessions{ get; set; }

}