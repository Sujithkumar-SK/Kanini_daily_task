namespace Backend.DTOs;

public class CompanyProfileDto
{
  public int CompanyProfileId { get; set; }
  public string CompanyName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public string? Website { get; set; }
  public bool IsActive { get; set; }
}

public class CompanyProfileCreateDto
{
  public string CompanyName { get; set; } = string.Empty;
  public string? Description { get; set; } = string.Empty;
  public string? Website { get; set; }
}

public class CompanyProfileUpdateDto
{
  public string? CompanyName { get; set; }
  public string? Description { get; set; }
  public string? Website { get; set; }
  public bool? IsActive { get; set; }
}
