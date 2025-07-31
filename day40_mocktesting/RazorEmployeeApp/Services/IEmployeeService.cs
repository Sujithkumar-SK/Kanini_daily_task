using RazorEmployeeApp.Models;
namespace RazorEmployeeApp.Services
{
  public interface IEmployeeService
  {
    List<Employee> GetAllEmployee();
  }
}