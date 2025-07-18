using System;
namespace day23
{
  class BooleanResult
  {
    public static void Main(string[] args)
    {
      int x, y;
      bool result;
      Console.Write("Enter the value for X: ");
      x = Convert.ToInt32(Console.ReadLine());
      Console.Write("Enter the value for Y : ");
      y = Convert.ToInt32(Console.ReadLine());
      result = x < y;
      Console.WriteLine($"The result of whether X is less than Y is {result}");
    }
  }
}