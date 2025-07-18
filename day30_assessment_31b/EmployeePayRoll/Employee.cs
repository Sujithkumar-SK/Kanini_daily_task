using System;
namespace PayRoll
{
  public abstract class Employee
  {
    public int Id;
    public string Name;
    public float BasicSalary;
    public float Bonus;
    public float NetSalary;

    public abstract float CalculateSalary(int id, string name, float basicSalary);
    public abstract float CalculateBonus(float salary, int criteria);
  }
}