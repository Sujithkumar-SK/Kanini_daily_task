using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Mark
{
  [Key]
  public int MarkId { get; set; }
  public string? Subject { get; set; }
  public int Score { get; set; }
  public int StudentId { get; set; }
  [ForeignKey("StudentId")]
  public Student? Student { get; set; }

}