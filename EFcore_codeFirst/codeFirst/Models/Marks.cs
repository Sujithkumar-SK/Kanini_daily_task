using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace codeFirst.Models;

public class Marks
{
  [Key]
  public int MarksId { get; set; }
  public string? Subject { get; set; }
  public int Score { get; set; }

  public int? StudentId { get; set; }

  [ForeignKey("StudentId")]

  public Student? Student { get; set; }
}
