using RazorEmployeeApp.Models;
using RazorEmployeeApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace RazorEmployeeApp.Pages
{
  public class EmployeesModel : PageModel
  {
    private readonly IEmployeeService? _service;
    public List<Employee> Employees { get; set; } = new();
    public EmployeesModel(IEmployeeService service)
    {
      _service = service;
    }
    public void OnGet()
    {
      Employees = _service!.GetAllEmployee();
    }
  }
}