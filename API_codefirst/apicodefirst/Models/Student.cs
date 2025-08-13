using System.ComponentModel.DataAnnotations;
public class Student
{
  [Key]
  public int Id { get; set; }
  public string? Name { get; set; }
  public int Age { get; set; }

  public ICollection<Mark>? Mark { get; set; } = new List<Mark>();
}