using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Resume
{
    [Key]
    public int ResumeId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public byte[] FileData { get; set; } = Array.Empty<byte>();

    [Required, MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public User User { get; set; } = null!;
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
