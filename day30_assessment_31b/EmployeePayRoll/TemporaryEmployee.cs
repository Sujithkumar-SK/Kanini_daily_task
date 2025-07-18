using System;
namespace PayRoll
{
  public class TemporaryEmployee : Employee
  {
    public int DailyWages;
    public int NoOfDays;

    public override float CalculateSalary(int id, string name, float basicSalary)
    {
      this.Id = id;
      this.Name = name;
      Console.Write("Enter Daily Wages: ");
      DailyWages = int.Parse(Console.ReadLine()!);
      Console.Write("Enter Number of Days: ");
      NoOfDays = int.Parse(Console.ReadLine()!);
      NetSalary = DailyWages * NoOfDays;
      Bonus = CalculateBonus(NetSalary, DailyWages);
      return NetSalary;
    }
    public override float CalculateBonus(float salary, int dailyWages)
    {
      if (dailyWages < 1000)
            return salary * 0.15f;
        else if (dailyWages >= 1000 && dailyWages < 1500)
            return salary * 0.12f;
        else if (dailyWages >= 1500 && dailyWages < 1750)
            return salary * 0.11f;
        else
            return salary * 0.08f;
    }
  }
}