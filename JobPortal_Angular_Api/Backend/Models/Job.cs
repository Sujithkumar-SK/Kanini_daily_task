using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Job
{
    [Key]
    public int JobId { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string EmploymentType { get; set; } = "Full-time";

    public decimal? Salary { get; set; }

    [Required]
    public int PostedBy { get; set; }

    public DateTime PostedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public User? Recruiter { get; set; } = null;
    public ICollection<Application> Applications { get; set; } = new List<Application>();
    public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
