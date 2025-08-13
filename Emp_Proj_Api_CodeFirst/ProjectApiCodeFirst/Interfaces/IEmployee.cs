public interface IEmployee
{
  Task<IEnumerable<Employee>> GetAllEmployee();
  Task<Employee> GetEmployeeById(int id);
  Task<Employee> AddEmployee(Employee employee);
  Task<Employee> UpdateEmployee(int id, Employee employee);
  Task<bool> DeleteEmployee(int id);
}