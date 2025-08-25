namespace Frontend.Models;

public class EnrollmentDto
{
    public int EnrollmentId { get; set; }

    // Add these ↓
    public int KidId { get; set; }
    public int CourseId { get; set; }

    public KidsDto? Kid { get; set; }
    public CourseDto? Course { get; set; }
}
