using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Employee
{
  [Key]
  public int EmployeeId { get; set; }

  [Required, StringLength(8)]
  public string EmployeeCode { get; set; }

  [Required, StringLength(150)]
  public string FullName { get; set; }

  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required, StringLength(150)]
  public string Designation { get; set; }

  [Required]
  public Decimal Salary { get; set; }

  [ForeignKey("ProjectId")]
  public int ProjectId { get; set; }
  
  public Project? Project { get; set; }
}