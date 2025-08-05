using System.ComponentModel.DataAnnotations;
namespace razor_codefirst.Models;

public class Student
{
  [Key]
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Place { get; set; }
}
