using System;
namespace PayRoll
{
  class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("1. Permanent\n2. Temporary\nEnter the type of Employee:");
      string type = Console.ReadLine()!.ToLower();
      Console.Write("Employee Id: ");
      int id = int.Parse(Console.ReadLine()!);
      Console.Write("Employee Name: ");
      string name = Console.ReadLine()!;

      if (type.Contains("per") || type == "permanent")
      {
        Console.Write("Enter the Basic Salary: ");
        float basicSalary = float.Parse(Console.ReadLine()!);
        PermanentEmployee PER = new PermanentEmployee();
        PER.CalculateSalary(id, name, basicSalary);
        Console.WriteLine("\nThe details are:");
        Console.WriteLine($"Employee Id: {PER.Id}");
        Console.WriteLine($"Employee Name: {PER.Name}");
        Console.WriteLine($"Basic Salary: {PER.BasicSalary}");
        Console.WriteLine($"PF: {PER.Pf}");
        Console.WriteLine($"Bonus: {PER.Bonus}");
        Console.WriteLine($"Net Salary: {PER.NetSalary}");
      }
      else if (type.Contains("tem") || type == "temporary")
      {
        TemporaryEmployee TEMP = new TemporaryEmployee();
        TEMP.CalculateSalary(id, name, 0);

        Console.WriteLine("\nThe details are:");
        Console.WriteLine($"Employee Id: {TEMP.Id}");
        Console.WriteLine($"Employee Name: {TEMP.Name}");
        Console.WriteLine($"Daily Wages: {TEMP.DailyWages}");
        Console.WriteLine($"Number of Days: {TEMP.NoOfDays}");
        Console.WriteLine($"Bonus: {TEMP.Bonus}");
        Console.WriteLine($"Net Salary: {TEMP.NetSalary}");
      }
      else
      {
        Console.WriteLine("Invalid Employee Type...");
      }
    }
  }
}