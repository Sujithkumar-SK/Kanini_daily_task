using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CompanyProfile
{
    [Key]
    public int CompanyId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public byte[]? Logo { get; set; }

    [MaxLength(255)]
    public string Website { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public User Recruiter { get; set; } = null!;
}
