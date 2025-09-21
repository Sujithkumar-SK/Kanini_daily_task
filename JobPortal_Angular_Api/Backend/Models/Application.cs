using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Application
{
    [Key]
    public int ApplicationId { get; set; }

    [Required]
    public int JobId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    [Required]
    public int ResumeId { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public Job Job { get; set; } = null!;
    public User Candidate { get; set; } = null!;
    public Resume Resume { get; set; } = null!;
}
