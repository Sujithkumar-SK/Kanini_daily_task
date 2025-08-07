using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace codeFirst.Models
{
  public class Student
  {
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public int MarksId { get; set; }

    [ForeignKey("MarksId")]
    public Marks? Marks { get; set; }
  }
}