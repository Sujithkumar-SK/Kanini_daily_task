namespace Frontend.Models;
public class CourseDto
{
  public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public string Instructor { get; set; }
    public string? CourseImage { get; set; }

    public List<EnrollmentDto> Enrollments { get; set; } = new();
}
