using System;

namespace day23
{
  class Something
  {
    public static void Main(String[] args)
    {
      Console.Write("Enter a Number: ");
      int num = Convert.ToInt32(Console.ReadLine());
      double square_out = FindSquare(Convert.ToDouble(num));
      double cube_out = FindCube(Convert.ToDouble(num));

      Console.WriteLine($"Square of {num} is {square_out}");
      Console.WriteLine($"Cube of {num} is {cube_out}");
    }

    static double FindSquare(double squ)
    {
      return squ * squ;
    }

    static double FindCube(double cub)
    {
      return cub*cub *cub;
    }
  }
}