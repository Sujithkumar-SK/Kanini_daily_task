using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Kid
{
    [Key]
    public int KidId { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    public int Age { get; set; }

    [MaxLength(30)]
    public string? Grade { get; set; }
    public byte[]? ProfileImage { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public User? User { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
}
