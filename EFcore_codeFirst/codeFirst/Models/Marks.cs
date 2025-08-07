using System;
using System.ComponentModel.DataAnnotations;
namespace codeFirst.Models;

public class Marks
{
  [Key]
  public int MarksId { get; set; }
  public string? Subject { get; set; }
  public IList<Student>? Students {get; set; }
}
