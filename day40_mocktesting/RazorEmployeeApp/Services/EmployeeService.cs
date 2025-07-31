using RazorEmployeeApp.Models;
namespace RazorEmployeeApp.Services
{
  public class EmployeeService : IEmployeeService
  {
    public List<Employee> GetAllEmployee()
    {
      return new List<Employee>
      {
        new Employee { Id = 1, Name = "John Doe", Department = "HR" },
        new Employee { Id = 2, Name = "Jane Smith", Department = "IT" },
        new Employee { Id = 3, Name = "Sam Brown", Department = "Finance" }
      };
    }
  }
}