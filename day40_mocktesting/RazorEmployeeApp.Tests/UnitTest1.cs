using RazorEmployeeApp.Models;
using RazorEmployeeApp.Services;
using RazorEmployeeApp.Pages;
using Moq;
namespace RazorEmployeeApp.Tests;

public class EmployeeTests
{
    [Fact]
    public void OnGet_ShouldLoadEmployees_NotNull()
    {
        var mock = new Mock<IEmployeeService>();
        mock.Setup(s => s.GetAllEmployee()).Returns(new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "Developer" }
        });

        var pageModel = new EmployeesModel(mock.Object);
        pageModel.OnGet();

        Assert.Null(pageModel.Employees);
    }

    [Fact]
    public void OnGet_ShouldLoadEmployees_CountCheck()
    {
        var mock = new Mock<IEmployeeService>();
        mock.Setup(s => s.GetAllEmployee()).Returns(new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "Developer" },
            new Employee { Id = 2, Name = "Jane Smith", Department = "Manager" }
        });

        var pageModel = new EmployeesModel(mock.Object);
        pageModel.OnGet();

        Assert.Equal(2, pageModel.Employees.Count);
    }

    [Fact]
    public void OnGet_ShouldLoadEmployees_NameCheck()
    {
        var mock = new Mock<IEmployeeService>();
        mock.Setup(s => s.GetAllEmployee()).Returns(new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "Developer" }
        });

        var pageModel = new EmployeesModel(mock.Object);
        pageModel.OnGet();

        Assert.Equal("John Doe", pageModel.Employees[0].Name);
    }
}
