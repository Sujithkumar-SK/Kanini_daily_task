namespace Backend.DTOs;

public class CreateCourseDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Duration { get; set; }
    public string? Instructor { get; set; }
    public byte[]? CourseImage { get; set; }
}

public class UpdateCourseDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Duration { get; set; }
    public string? Instructor { get; set; }
    public byte[]? CourseImage { get; set; }
}
