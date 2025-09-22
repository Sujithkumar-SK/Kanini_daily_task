namespace Backend.DTOs
{
  public class JobSearchDto
  {
    public string? Keyword { get; set; }
    public string? Location { get; set; }
    public string? EmploymentType { get; set; }
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public List<string>? Skills { get; set; }
  }

  public class JobDto
  {
    public int JobId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string PostedBy { get; set; } = string.Empty;   // Recruiter name
    public List<string> Skills { get; set; } = new();
  }
}
