using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public enum DetailType
{
    Tenth,
    Twelfth,
    Diploma,
    BE,
    BSc,
    BCom,
    PG,
    Certification,
    Skill,
}

public class UserDetail
{
    public int UserDetailId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public DetailType DetailType { get; set; }

    [Required, MaxLength(255)]
    public string Value { get; set; } = string.Empty;

    public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public User User { get; set; } = null!;
}
