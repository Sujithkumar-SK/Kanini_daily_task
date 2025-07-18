using System;
namespace PayRoll
{
  public class PermanentEmployee : Employee
  {
    public int Pf;
    public override float CalculateSalary(int id, string name, float basicSalary)
    {
      this.Id = id;
      this.Name = name;
      this.BasicSalary = basicSalary;
      Console.Write(" Enter the PF: ");
      Pf = int.Parse(Console.ReadLine()!);

      Bonus = CalculateBonus(BasicSalary, Pf);
      NetSalary = BasicSalary  - Pf;
      return NetSalary;
    }
    public override float CalculateBonus(float salary, int pf)
    {
      if (pf < 1000)
        return salary * 0.10f;
      else if (pf >= 1000 && pf < 1500)
        return salary * 0.115f;
      else if (pf >= 1500 && pf < 1800)
        return salary * 0.12f;
      else
        return salary * 0.15f;
    }
  }
}