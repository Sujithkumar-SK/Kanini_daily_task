using System;
namespace Maintain
{
  class Reverse
  {
    public static void Main(string[] args)
    {
      Console.Write("Enter the string: ");
      string input = Console.ReadLine();

      string[] space = input.Split(' ');

      Array.Reverse(space);
      string rev = string.Join(" ", space);

      Console.WriteLine("Reversed String is: " + rev);
    }
  }
}