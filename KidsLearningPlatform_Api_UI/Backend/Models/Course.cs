using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Course
{
  [Key]
  public int CourseId { get; set; }

  [Required, MaxLength(100)]
  public string? Title { get; set; }

  public string? Description { get; set; }

  public string? Duration { get; set; }

  [Required, MaxLength(50)]
  public string? Instructor { get; set; }

  public byte[]? CourseImage { get; set; }
  
  public ICollection<Enrollment>?Enrollments { get; set; }
}