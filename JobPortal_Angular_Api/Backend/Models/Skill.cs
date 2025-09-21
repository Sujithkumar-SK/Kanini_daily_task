using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Skill
{
    [Key]
    public int SkillId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
