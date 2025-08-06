using System.ComponentModel.DataAnnotations;

namespace console_codefirst.Models;

public class Students
{
  [Key]
  public int Id { get; set; }
  public string? Name { get; set; }
}
