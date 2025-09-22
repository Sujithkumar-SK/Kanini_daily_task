namespace Backend.DTOs;

public class ApplicationCreateDto
{
  public int JobId { get; set; }
  public int ResumeId { get; set; }   // Resume already uploaded by candidate
}

public class ApplicationResponseDto
{
  public int ApplicationId { get; set; }
  public string CandidateName { get; set; } = string.Empty;
  public string JobTitle { get; set; } = string.Empty;
  public string Status { get; set; } = string.Empty;
  public DateTime AppliedOn { get; set; }
}

public class ApplicationUpdateDto
{
  public string Status { get; set; } = "Applied";
  public bool IsActive { get; set; } = true;
}
