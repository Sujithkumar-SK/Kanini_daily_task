public class EnrollmentDto
{
    public int EnrollmentId { get; set; }
    public int KidId { get; set; }
    public string KidName { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public DateTime EnrolledDate { get; set; }
}

public class CreateEnrollmentDto
{
    public int KidId { get; set; }
    public int CourseId { get; set; }
}
