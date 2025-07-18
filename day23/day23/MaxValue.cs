using System;
namespace day23
{
  class MaxValue
  {
    public static void Main(string[] args)
    {
      sbyte number = 125;
      Console.WriteLine($"Value of number: {number}");
      number = sbyte.MaxValue;
      Console.WriteLine($"Largest value stored in a signed byte: {number}");
    }
  }
}