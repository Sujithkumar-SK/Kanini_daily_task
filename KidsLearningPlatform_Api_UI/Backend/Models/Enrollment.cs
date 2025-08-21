using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Enrollment
{
  [Key]
  public int EnrollmentId { get; set; }

  [ForeignKey("Kid")]
  public int KidId { get; set; }

  [ForeignKey("Course")]
  public int CourseId { get; set; }

  public DateTime EnrolledDate { get; set; } = DateTime.Now;

  public Kid? Kid { get; set; }
  public Course? Course { get; set; }
}