namespace Backend.DTOs;

public class JobCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = "Full-time";
    public decimal? Salary { get; set; }
    public bool IsActive { get; set; } = true;
}
