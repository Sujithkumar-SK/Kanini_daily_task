using System;
using Microsoft.Data.SqlClient;

class Program
{
  public static void Main(string[] args)
  {
    String connect = "Server=SKEMPIRE;Database=CompanyDB;Integrated Security=True;TrustServerCertificate=True";
    SqlConnection con = new SqlConnection(connect);
    con.Open();
    while (true)
    {
      Console.WriteLine("\n1. Add Employee\n2. View Employees\n3. Update Employee\n4. Delete Employee\n5. Search by Role\n6. Exit");
      Console.Write("Enter your choice: ");
      string choice = Console.ReadLine()!;
      switch (choice)
      {
        case "1":
          AddEmployee(con); break;
        case "2":
          ViewEmployee(con); break;
        case "3":
          UpdateEmployee(con); break;
        case "4":
          DeleteEmployee(con); break;
        case "5":
          SerachByRole(con); break;
        case "6":
          con.Close();
          Console.Write("Bye..."); return;
        default:
          Console.WriteLine("Invalid Input You fool..."); break;
      }
    }
  }

  static void AddEmployee(SqlConnection con)
  {
    Console.Write("Enter your Name: ");
    string name = Console.ReadLine()!;
    Console.Write("Enter youtr Role: ");
    string role = Console.ReadLine()!;
    Console.Write("Enter your Salary: ");
    decimal sal = Convert.ToInt32(Console.ReadLine());
    string query = "insert into employee values(@name, @role,@salary)";
    using SqlCommand cmd = new SqlCommand(query, con);
    cmd.Parameters.AddWithValue("@name", name);
    cmd.Parameters.AddWithValue("@role", role);
    cmd.Parameters.AddWithValue("@salary", sal);
    int result = cmd.ExecuteNonQuery();
    Console.WriteLine(result > 0 ? "Employee Added!" : "Insert Failed");
  }
  static void ViewEmployee(SqlConnection con)
  {
    string query = "select * from employee";
    using SqlCommand cmd = new SqlCommand(query, con);
    Console.WriteLine("\n ID\tName\t\tRole\t\tSalary");
    using SqlDataReader read = cmd.ExecuteReader();
    while (read.Read())
    {
      Console.WriteLine($"{read[0]}\t{read[1]}\t\t{read[2]}\t\t{read[3]}");
    }
  }
  static void UpdateEmployee(SqlConnection con)
  {
    Console.Write("Enter ID to update: ");
    int id = int.Parse(Console.ReadLine()!);
    Console.Write("Enter New Name: ");
    string name = Console.ReadLine()!;
    Console.Write("Enter New Role: ");
    string role = Console.ReadLine()!;
    Console.Write("Enter New Salary: ");
    decimal salary = decimal.Parse(Console.ReadLine()!);
    string query = "update employee set name=@name, role=@role, salary=@salary where id=@id";
    using SqlCommand cmd = new SqlCommand(query, con);
    cmd.Parameters.AddWithValue("@id", id);
    cmd.Parameters.AddWithValue("@name", name);
    cmd.Parameters.AddWithValue("@role", role);
    cmd.Parameters.AddWithValue("@salary", salary);
    int result = cmd.ExecuteNonQuery();
    Console.WriteLine(result > 0 ? "Employee Updated" : "Employee update failed");
  }
  static void DeleteEmployee(SqlConnection con)
  {
    Console.Write("Enter the Employee Id: ");
    int id = Convert.ToInt32(Console.ReadLine());
    string query = "delete from employee where id=@id";
    using SqlCommand cmd = new SqlCommand(query, con);
    cmd.Parameters.AddWithValue("@id", id);
    int result = cmd.ExecuteNonQuery();
    Console.WriteLine(result > 0 ? "Employee Record Deleted" : "Delete Failed");
  }
  static void SerachByRole(SqlConnection con)
  {
    Console.Write("Enter the Role: ");
    string role = Console.ReadLine()!;
    string query = "select * from employee where role like @role";
    using SqlCommand cmd = new SqlCommand(query, con);
    cmd.Parameters.AddWithValue("@role", role);
    using SqlDataReader read = cmd.ExecuteReader();
    Console.WriteLine("\nID\tName\t\tRole\t\t\tSalary");
    while (read.Read())
    {
      Console.WriteLine($"{read[0]}\t{read[1]}\t\t{read[2]}\t\t{read[3]}");
    }
  }
}