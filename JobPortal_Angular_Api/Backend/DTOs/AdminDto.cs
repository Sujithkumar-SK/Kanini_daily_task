namespace Backend.DTOs;

public class UserSummaryDto
{
  public int UserId { get; set; }
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public bool IsActive { get; set; }
}

public class RecruiterSummaryDto
{
  public int RecruiterId { get; set; }
  public string CompanyName { get; set; } = string.Empty;
  public string Website { get; set; } = string.Empty;
  public bool IsActive { get; set; }
}

public class AnalyticsDto
{
  public int TotalUsers { get; set; }
  public int TotalRecruiters { get; set; }
  public int TotalCandidates { get; set; }
  public int JobsPosted { get; set; }
  public int ApplicationsSubmitted { get; set; }
}
