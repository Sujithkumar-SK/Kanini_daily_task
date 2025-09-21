using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public enum UserRole
{
    Admin,
    Recruiter,
    Candidate,
}

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public byte[]? ProfileImage { get; set; }

    public ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
    public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
    public ICollection<Application> Applications { get; set; } = new List<Application>();
    public ICollection<Job> Jobs { get; set; } = new List<Job>();

    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
