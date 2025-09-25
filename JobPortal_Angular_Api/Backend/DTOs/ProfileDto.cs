namespace Backend.DTOs;

public class UpdateProfileDto
{
  public string? FullName { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }
  public string? ProfileImage { get; set; }
  public List<string>? Skills { get; set; }  // Using UserDetail with DetailType.Skill
  public List<string>? Qualifications { get; set; } // UserDetail with DetailType.Tenth, Twelfth, BE, etc.
}

public class UserDetailDto
{
  public int UserDetailId { get; set; }
  public string Type { get; set; } = string.Empty;
  public string Value { get; set; } = string.Empty;
}
