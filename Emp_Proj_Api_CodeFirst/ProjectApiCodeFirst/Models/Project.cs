using System.ComponentModel.DataAnnotations;
public class Project
{
  [Key]
  public int ProjectId { get; set; }

  [Required,StringLength(10)]
  public string ProjectCode { get; set; }

  [Required, StringLength(100)]
  public string ProjectName { get; set; }

  [Required]
  public DateTime StartDate { get; set; }
  public DateTime? EndDate { get; set; }

  [Required]
  public Decimal Budget { get; set; }

  public ICollection<Employee> Employees { get; set; } = new List<Employee>();


}